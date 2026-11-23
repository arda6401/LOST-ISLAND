using LostIsland.Combat;
using Mirror;
using UnityEngine;

namespace LostIsland.Player
{
    [DisallowMultipleComponent]
    public class PlayerEntity : NetworkBehaviour
    {
        public Health health { get; private set; }
        public PlayerInventory Inventory { get; private set; }

        public bool IsDead => health.IsDead;



        private void Awake()
        {
            health = GetComponent<Health>();
            Inventory = GetComponent<PlayerInventory>();
            if (health == null)
            {
                Debug.LogError("[PLAYER ENTİTY] health is not added");
               
            }
            if (Inventory == null)
            {
                Debug.LogError("[PLAYER ENTİTY] Inventory is not added");
                
            }


        }

        public override void OnStartClient()
        {
            PlayerRegistry.Add(this);
        }
        public override void OnStopClient()
        {

            PlayerRegistry.Remove(this);
        }

    }
}