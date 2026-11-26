using System;
using LostIsland.Player;
using UnityEngine;
namespace LostIsland.Core
{
    public static class LocalPlayerEvents
    {
        public static event Action<Transform> Spawned;
        public static event Action Despawned;
        public static PlayerEntity Player;


        public static Transform Current { get; private set; }

        public static void RaiseSpawned(Transform player)
        {
            Current = player;
            Player = player.GetComponent<PlayerEntity>();
            Spawned?.Invoke(player);
        }

        public static void RaiseDespawned()
        {
            Current = null;
            Despawned?.Invoke();
        }

    }
}
