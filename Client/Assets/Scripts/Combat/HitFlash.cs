
using Mirror;
using UnityEngine;

namespace LostIsland.Combat
{

    [RequireComponent(typeof(Health))]
    public class HitFlash : NetworkBehaviour
    {
        [SerializeField] private Renderer targetRenderer;
        [SerializeField] private Color flashColor = new Color(1f, 0.2f, 0.2f, 1f);
        [SerializeField] private float duration = 0.12f;

        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        private Health health;
        private MaterialPropertyBlock block;
        private int lastHealth;
        private float flashEndTime;
        private bool flashing;




        void Awake()
        {
            health = GetComponent<Health>();
            block = new MaterialPropertyBlock();

        }


        public override void OnStartClient()
        {
            lastHealth = health.Current;
            health.HealthChanged += OnHealthChanged;
        }
        public override void OnStopClient()
        {
            health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int current, int max)
        {
            if (current < lastHealth) Flash();
            lastHealth = current;
        }


        private void Flash()
        {
            block.SetColor(BaseColorId, flashColor);
            targetRenderer.SetPropertyBlock(block);
            flashEndTime = Time.time + duration;
            flashing = true;

        }

        [ClientCallback]
        private void Update()
        {
            if (!flashing || Time.time < flashEndTime) return;

            targetRenderer.SetPropertyBlock(null); // asıl renge dön
            flashing = false;
        }

    }
}
