using System;
using System.Collections.Generic;
using LostIsland.Combat;
using LostIsland.Player;
using LostIsland.Weapons;
using Mirror;
using UnityEngine;
using UnityEngine.UI;


namespace LostIsland.Items
{

    [RequireComponent(typeof(SphereCollider))]
    public class WorldItem : HoldInteracttable
    {
        [Header("data")]
        [SerializeField] private static ItemDatabase database;
        [SerializeField] private GameObject ModelRoot;

        [SerializeField] private int startQuantity = 1;

        [SyncVar] private ushort itemId;
        [SyncVar] private int quantity;



        public ItemDefinition Definition => database.Get(itemId);

        protected override float HoldDuration => Definition.pickupTime;


        protected override bool Interact(PlayerEntity player)
        {
            if (player.Inventory.TryPickup(itemId))
            {
                NetworkServer.Destroy(gameObject);
                return true;
            }

            return false;
        }

        [Server]
        public static WorldItem SpawnWorldItem(ushort id, Vector3 pos)
        {
            if (!database.TryGetWeapon(id, out WeaponDefinition weapon))
                return null;

            GameObject root = new GameObject(weapon.displayName);
            root.transform.position = pos;
            SphereCollider collider = root.AddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = 1f;

            WorldItem worldItem = root.AddComponent<WorldItem>();
            worldItem.itemId = id;
            worldItem.ModelRoot = weapon.ItemPrefab;

            NetworkServer.Spawn(root);
            return worldItem;


            
        }


        #region Client


        public override void OnStartClient()
        {
            ItemDefinition def = Definition;
            if (def == null) return;


        }



        #endregion
    }
}