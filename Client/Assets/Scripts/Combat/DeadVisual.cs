using Mirror;
using UnityEngine;

namespace LostIsland.Combat
{

    [RequireComponent(typeof(Health))]
    public class DeadVisual : NetworkBehaviour
    {
        [SerializeField] private Transform visual;
        [SerializeField] private Vector3 deadLocalPosition = new Vector3(0f, 0f, 0f);
        [SerializeField] private Vector3 deadLocalEuler = new Vector3(90f, 0f, 0f);

        private Health health;

        private void Awake() => health = GetComponent<Health>();

        public override void OnStartClient()
        {
            health.Died += Apply;
            if (health.IsDead) Apply(); // ölümden sonra bağlanan istemci için
        }

        public override void OnStopClient() => health.Died -= Apply;

        private void Apply()
        {
            visual.localPosition = deadLocalPosition;
            visual.localRotation = Quaternion.Euler(deadLocalEuler);
         
        }
    }
}