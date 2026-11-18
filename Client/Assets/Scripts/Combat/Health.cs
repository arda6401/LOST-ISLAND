using System;
using Mirror;
using UnityEngine;


namespace LostIsland.Combat
{
    public class Health : NetworkBehaviour, IDamageable
    {

        [SerializeField] private int maxHealth = 100;


        [SyncVar(hook = nameof(OnCurrentChanged))]
        private int current;


        public int Current => current;


        public int Max => maxHealth;


        public bool IsDead => current <= 0;



        public event Action<int, int> HealthChanged;
        public event Action Died;
        public event Action ServerDied;              // sadece sunucuda (maç sistemi için)




        public override void OnStartServer()
        {
            current = maxHealth;
        }


        [Server]
        public void TakeDamage(int amount)
        {
            if (IsDead || amount <= 0) return;

            current = Mathf.Max(0, current - amount);
            Debug.Log("[PLAYER] DAmaged!!");

            if (current == 0)
                ServerDied?.Invoke();

        }



        private void OnCurrentChanged(int oldvalue, int newvalue)
        {
            HealthChanged?.Invoke(newvalue, maxHealth);

            if (oldvalue > 0 && newvalue <= 0)
            {
                Died?.Invoke();
            }
        }


        [ContextMenu("Debug: 10 hasar")]
        private void DebugDamage()
        {
            if (isServer) TakeDamage(10);
        }

        private void OnGUI()
        {
            if (!isLocalPlayer) return;
            GUI.Label(new Rect(20, 20, 200, 30), $"HP: {current} / {maxHealth}");
        }
    }
}