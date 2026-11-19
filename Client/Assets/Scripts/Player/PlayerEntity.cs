using LostIsland.Combat;
using Mirror;
using UnityEngine;

namespace LostIsland.Player
{
    [DisallowMultipleComponent]
    public class PlayerEntity : NetworkBehaviour
    {
        public Health health { get; private set; }

        public bool IsDead => health.IsDead;



        private void Awake()
        {
            health = GetComponent<Health>();
            if (health == null)
            {
                Debug.LogError("[PLAYER ENTİTY] health is not added");
                return;
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