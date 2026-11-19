using UnityEngine;
using LostIsland.Combat;
using LostIsland.Core;
using LostIsland.Player;

namespace LostIsland.UI
{
    // İSTEMCİ: yerel oyuncu ölünce mobil kontrolleri gizler.
    public class MobileControlsVisibility : MonoBehaviour
    {
        [SerializeField] private GameObject controlsRoot;

        private PlayerEntity localplayer;

        private void OnEnable()
        {
            LocalPlayerEvents.Spawned += OnLocalSpawned;
            LocalPlayerEvents.Despawned += OnLocalDespawned;
        }

        private void OnDisable()
        {
            LocalPlayerEvents.Spawned -= OnLocalSpawned;
            LocalPlayerEvents.Despawned -= OnLocalDespawned;
            Unsubscribe();
        }

        private void OnLocalSpawned(Transform player)
        {
            localplayer = player.GetComponent<PlayerEntity>();
            localplayer.health.Died += OnLocalDied;
            controlsRoot.SetActive(!localplayer.IsDead);
        }

        private void OnLocalDespawned() => Unsubscribe();

        private void OnLocalDied() => controlsRoot.SetActive(false);

        private void Unsubscribe()
        {
            if (localplayer != null) localplayer.health.Died -= OnLocalDied;
            localplayer = null;
        }
    }
}