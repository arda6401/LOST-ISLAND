using System;
using LostIsland.Combat;
using LostIsland.Core;
using LostIsland.Player;
using UnityEngine;




namespace LostIsland.Match
{
    public class SpectatorController : MonoBehaviour
    {
        [SerializeField] private CameraTargetBinder cameraBinder;
        [SerializeField] private GameObject SpectatorUI;



        private PlayerEntity LocalPlayer;
        private PlayerEntity followed;
        private bool spectating;


        private void Awake()
        {
            if (SpectatorUI != null) SpectatorUI.SetActive(false);
        }

        private void OnEnable()
        {
            LocalPlayerEvents.Spawned += OnLocalSpawned;
            LocalPlayerEvents.Despawned += OnLocalDespawned;
            PlayerRegistry.Changed += OnRegistryChanged;
        }



        private void OnDisable()
        {
            LocalPlayerEvents.Spawned -= OnLocalSpawned;
            LocalPlayerEvents.Despawned -= OnLocalDespawned;
            PlayerRegistry.Changed -= OnRegistryChanged;
            StopSpectating();
        }

        private void OnLocalSpawned(Transform player)
        {
            LocalPlayer = player.GetComponent<PlayerEntity>();
            LocalPlayer.health.Died += OnLocalDied;
            if (LocalPlayer.health.IsDead)
                OnLocalDied();
        }
        private void OnLocalDespawned()
        {
            if (LocalPlayer != null) LocalPlayer.health.Died -= OnLocalDied;

            LocalPlayer = null;
            StopSpectating();

        }
        private void OnLocalDied()
        {
            spectating = true;
            if (SpectatorUI != null) SpectatorUI.SetActive(true);
            FollowNext();
        }

        private void OnRegistryChanged()
        {
            if (spectating && (followed == null || PlayerRegistry.IndexOf(followed) < 0))
                FollowNext();
        }

        private void OnFollowedDied() => FollowNext();
        public void FollowNext()
        {
            if (!spectating) return;

            int start = followed != null ? PlayerRegistry.IndexOf(followed) : -1;
            int count = PlayerRegistry.Count;

            for (int step = 1; step <= count; step++)
            {
                int index = (start + step) % count;
                if (index < 0) index += count;
                PlayerEntity candidate = PlayerRegistry.Get(index);
                if (candidate == null || candidate == LocalPlayer || candidate.IsDead)
                    continue;


                Follow(candidate);
                return;
            }

            // nothing for follow...
        }

        private void Follow(PlayerEntity player)
        {
            if (followed != null) followed.health.Died -= OnFollowedDied;
            followed = player;
            followed.health.Died += OnFollowedDied;
            cameraBinder.SetTarget(followed.transform);
        }

        private void StopSpectating()
        {
            if (followed != null) followed.health.Died -= OnFollowedDied;
            followed = null;
            spectating = false;
            if (SpectatorUI != null) SpectatorUI.SetActive(false);
        }






    }
}