using System;
using System.Collections.Generic;
using LostIsland.Combat;
using LostIsland.Player;
using Mirror;
using UnityEngine;


namespace LostIsland.Items
{

    [RequireComponent(typeof(SphereCollider))]
    public class WorldItem : NetworkBehaviour
    {
        [Header("data")]
        [SerializeField] private ItemDatabase database;
        [SerializeField] private ItemDefinition startItem;
        [SerializeField] private int startQuantity = 1;



        [Header("virtual")]
        [SerializeField] private Renderer visual;



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
                Pickup(leader.player, item);
        }



        [Server]
        private void Pickup(PlayerEntity player, ItemDefinition def)
        {
            taken = true;
            claims.Clear();
            Debug.Log($"[Server] {player.name} picked up {def.displayName} x{quantity}");
            NetworkServer.Destroy(gameObject);
        }






        #endregion



        #region Client
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        public override void OnStartClient()
        {
            ItemDefinition def = Definition;
            if (def == null || visual == null) return;

            MaterialPropertyBlock block = new MaterialPropertyBlock();
            block.SetColor(BaseColorId, def.worldColor);
            visual.SetPropertyBlock(block);
        }






        [Header("Bekleme halkası")]

        [SerializeField] private LineRenderer ring;
        [SerializeField] private float ringRadius = 0.6f;
        [SerializeField] private float ringHeight = 0.5f;

        [SerializeField] private Color ownColor = Color.white;
        [SerializeField] private Color otherColor = new Color(1f, 0.3f, 0.3f);
        private const int ringSegments = 48;

        private void Awake()
        {
            if(ring == null)
                return;

            ring.useWorldSpace = false;
            ring.loop = false;
            ring.enabled = false;
            ring.positionCount = 0;
        }

        private void OnProgressChanged(float oldvalue, float newvalue)
        {
            UpdateProgressRing(newvalue);
        }

        private void UpdateProgressRing(float elapsedSecond)
        {
            if (ring == null) return;


            ItemDefinition item = Definition;
            if (item == null || item.pickupTime <= 0 || elapsedSecond <= 0)
            {
                ring.enabled = false;
                return;
            }

           
            DrawProgressFill(elapsedSecond);


        }

        private void DrawProgressFill(float fill)
        {
            if (ring == null) return;

            fill = Mathf.Clamp01(fill);

            if (fill <= 0)
            {
                ring.enabled = false;
                return;
            }

            int segments = Mathf.Clamp(ringSegments, 3, 256);
            int visiblesegment = Mathf.Clamp(Mathf.CeilToInt(segments * fill), 1, segments);

            ring.enabled = true;
            ring.positionCount = visiblesegment + 1;
            for (int i = 0; i <= visiblesegment; i++)
            {
                float angle = (float)i / segments * Mathf.PI * 2f;

                Vector3 point = new Vector3(
                    Mathf.Cos(angle) * ringRadius,
                    ringHeight,
                    Mathf.Sin(angle) * ringRadius
                );


                ring.SetPosition(i, point);

            }
        }

        private void UpdateItemVisual()
        {
            ItemDefinition item = Definition;
            if (item == null || visual == null)
                return;

            MaterialPropertyBlock block = new MaterialPropertyBlock();
            visual.GetPropertyBlock(block);
            block.SetColor(BaseColorId, item.worldColor);
            visual.SetPropertyBlock(block);
        }



        #endregion
    }
}