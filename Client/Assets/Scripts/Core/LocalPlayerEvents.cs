using System;
using UnityEngine;
namespace LostIsland.Core
{
    public static class LocalPlayerEvents
    {
        public static event Action<Transform> Spawned;
        public static event Action Despawned;


        public static void RaiseSpawned(Transform player) => Spawned?.Invoke(player);
        public static void RaiseDespawned() => Despawned?.Invoke();

    }
}
