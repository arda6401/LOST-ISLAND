using System.Collections.Generic;
using LostIsland.Items;
using LostIsland.Player;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public abstract class HoldInteracttable : NetworkBehaviour
{
    protected abstract float HoldDuration { get; }
    protected abstract bool Interact(PlayerEntity player);
    [SyncVar] private float syncedProgress;

    private class Claim
    {
        public PlayerEntity player;
        public float Elapsed;
    }
    private readonly List<Claim> claims = new List<Claim>();
    private bool done;

    protected bool IsDone => done;
    private void Reset()
    {
        SphereCollider c = GetComponent<SphereCollider>();
        c.isTrigger = true;
        c.radius = 1f;
    }

    private bool FindPlayer(PlayerEntity player, out Claim claim)
    {
        claim = null;
        for (int i = 0; i < claims.Count; i++)
        {
            if (claims[i].player == player)
            {
                claim = claims[i];
                return true;
            }
        }
        return false; ;
    }

    private void OnTriggerEnter(Collider other)
    {


        if (!isServer || done) return;

        PlayerEntity player = other.GetComponentInParent<PlayerEntity>();
        if (player == null)
        {
            Debug.Log("[WORLDITEM:ONTRİGGER] player null...");
            return;
        }

        if (FindPlayer(player, out Claim claim)) return;



        claims.Add(new Claim
        {
            player = player
        });

    }
    private void OnTriggerExit(Collider other)
    {
        if (!isServer) return;

        PlayerEntity player = other.GetComponentInParent<PlayerEntity>();
        if (player == null) return;

        if (FindPlayer(player, out Claim claim))
            claims.Remove(claim);
    }




    [ServerCallback]
    private void FixedUpdate()
    {
        if (done) return;

        // Ölü veya yok olmuş oyuncuları temizle
        for (int i = claims.Count - 1; i >= 0; i--)
            if (claims[i].player == null || claims[i].player.IsDead)
                claims.RemoveAt(i);

        if (claims.Count == 0)
        {
            if (syncedProgress != 0f) syncedProgress = 0f;
            return;
        }

        float duration = HoldDuration;
        if (float.IsInfinity(duration) || float.IsNaN(duration)) return;

        float dt = Time.fixedDeltaTime;
        Claim leader = claims[0];
        /*  foreach (Claim claim in claims)
          {
              claim.Elapsed += dt;
              if (claim.Elapsed > leader.Elapsed) leader = claim;
          }*/

        syncedProgress = duration > 0f
            ? Mathf.Clamp01(leader.Elapsed / duration)
            : 1f;

        if (leader.Elapsed < duration) return;

        if (Interact(leader.player))
        {
            done = true;
            claims.Clear();
        }
        else
        {
            leader.Elapsed = 0f; // başarısızsa her tick tekrar denemesin
        }
    }



    #region Client

    [SerializeField] private Image progressBar;
    [SerializeField] private Color ownColor = Color.green;
    [SerializeField] private Color otherColor = Color.gray;
    [SerializeField] private float smoothSpeed = 15f;
    private float targetProgress;
    private float displayedProgress;

    protected virtual void Awake()
    {
        if (progressBar != null) progressBar.fillAmount = 0f;
    }

    private void OnProgressChanged(float oldValue, float newValue)
    {
        targetProgress = newValue;
        if (newValue <= 0f) displayedProgress = 0f; // sıfırlanınca anında boşalt
    }

    [ClientCallback]
    private void Update()
    {
        if (progressBar == null || !progressBar.IsActive()) return;

        float t = 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime);
        displayedProgress = Mathf.Lerp(displayedProgress, targetProgress, t);
        progressBar.fillAmount = displayedProgress;
    }

    #endregion


}
