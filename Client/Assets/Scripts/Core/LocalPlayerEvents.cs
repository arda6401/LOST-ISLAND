using System;
using UnityEngine;
namespace LostIsland.Core
{
    public static class LocalPlayerEvents
    {
        public static event Action<Transform> Spawned;
        public static event Action Despawned;


        public static Transform Current { get; private set; }

        public static void RaiseSpawned(Transform player)
        {
            Current = player;
            Spawned?.Invoke(player);
        }

        public static void RaiseDespawned()
        {
            Current = null;
            Despawned?.Invoke();
        }

    }
}
