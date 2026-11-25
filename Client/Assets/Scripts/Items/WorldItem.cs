using System;
using System.Collections.Generic;
using LostIsland.Combat;
using LostIsland.Player;
using Mirror;
using UnityEngine;
using UnityEngine.UI;


namespace LostIsland.Items
{

    [RequireComponent(typeof(SphereCollider))]
    public class WorldItem : NetworkBehaviour
    {
        [Header("data")]
        [SerializeField] private ItemDatabase database;
        [SerializeField] private ItemDefinition startItem;
        [SerializeField] private int startQuantity = 1;







        [SyncVar] private ushort itemId;
        [SyncVar] private int quantity;



        public ItemDefinition Definition => database.Get(itemId);



        private void Reset()
        {
            SphereCollider c = GetComponent<SphereCollider>();
            c.isTrigger = true;
            c.radius = 1f;
        }


        #region  Server

        [SyncVar(hook = nameof(OnProgressChanged))]
        private float syncedProgress;


        private const int RingSegments = 48;

        private class Claim
        {
            public PlayerEntity player;
            public float Elapsed;
        }

        private readonly List<Claim> claims = new List<Claim>();

        private bool taken;


        public override void OnStartServer()
        {
            if (itemId == ItemDatabase.None && startItem != null)
            {
                itemId = database.GetId(startItem);
                quantity = startQuantity;
            }
        }


        private void OnTriggerEnter(Collider other)
        {


            if (!isServer || taken) return;

            PlayerEntity player = other.GetComponentInParent<PlayerEntity>();
            if (player == null)
            {
                Debug.Log("[WORLDITEM:ONTRİGGER] player null...");
                return;
            }

            foreach (Claim c in claims)
                if (c.player == player) return;



            claims.Add(new Claim
            {
                player = player
            });

        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("[WORLDITEM:ONTRİGGER] çıkıldı");

            if (!isServer) return;
            PlayerEntity player = other.GetComponentInParent<PlayerEntity>();
            if (player == null)
            {
                Debug.Log("[WORLDITEM:ONTRİGGER] player null...");
                return;
            }



            for (int i = claims.Count - 1; i >= 0; i--)
                if (claims[i].player == player) claims.RemoveAt(i);


        }



        [ServerCallback]
        private void FixedUpdate()
        {
            if (taken)
            {
                return;
            }

            for (int i = claims.Count - 1; i >= 0; i--)
            {
                if (claims[i].player == null || claims[i].player.IsDead)
                    claims.RemoveAt(i);
            }
            if (claims.Count == 0)
            {
                if (syncedProgress != 0)
                    syncedProgress = 0;

                return;
            }


            ItemDefinition item = Definition;
            if (item == null) return;
            float dt = Time.fixedDeltaTime;

            Claim leader = claims[0];


            foreach (Claim claim in claims)
                claim.Elapsed += dt;



            syncedProgress = item.pickupTime > 0f
         ? Mathf.Clamp01(leader.Elapsed / item.pickupTime)
         : 1f;



            if (leader.Elapsed >= item.pickupTime)
                if (!Pickup(leader.player))
                    leader.Elapsed = 0;
        }



        [Server]
        private bool Pickup(PlayerEntity player)
        {

            if (player.Inventory == null) return false;
            if (!player.Inventory.TryPickup(itemId)) return false;

            taken = true;
            claims.Clear();
            Debug.Log($"[Server] {player.name} picked up {Definition.displayName} x{quantity}");
            NetworkServer.Destroy(gameObject);
            return true;
        }






        #endregion



        #region Client












        [SerializeField] private Image ProgressBar;
        private float targetProgress;
        private float displayedProgress;
        [SerializeField] private Color ownColor = Color.green;
        [SerializeField] private Color otherColor = Color.gray;
        private const int ringSegments = 48;

        private void Awake()
        {
            if (ProgressBar == null)
                return;

            ProgressBar.fillAmount = 0;
        }
        public override void OnStartClient()
        {
            ItemDefinition def = Definition;
            if (def == null) return;


        }

        private void OnProgressChanged(float oldvalue, float newvalue)
        {
           // Debug.Log($"[Ring] {Time.time:F2} -> {newvalue:F2}");
            targetProgress = newvalue;
            if (newvalue <= 0) displayedProgress = 0;
        }

        [ClientCallback]

        private void Update()
        {
            if (ProgressBar == null || !ProgressBar.IsActive()) return;

            float t = 1f - Mathf.Exp(-15 * Time.deltaTime);
            displayedProgress = Mathf.Lerp(displayedProgress, targetProgress, t);
            ProgressBar.fillAmount = displayedProgress;
        }






        #endregion
    }
}