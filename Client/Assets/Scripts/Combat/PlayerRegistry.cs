using System;
using System.Collections.Generic;
using LostIsland.Player;
using UnityEngine;


namespace LostIsland.Combat
{
    public static class PlayerRegistry
    {
        private static readonly List<PlayerEntity> players = new List<PlayerEntity>();

        public static int Count => players.Count;
        public static int IndexOf(PlayerEntity p) => players.IndexOf(p);
        public static PlayerEntity Get(int index) => players[index];

        public static event Action Changed;


        public static void Add(PlayerEntity player)
        {
            if (players.Contains(player)) return;

            players.Add(player);
            Changed?.Invoke();

        }

        public static void Remove(PlayerEntity player)
        {
            if (players.Remove(player))
                Changed?.Invoke();
        }




        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetOnLoad()
        {
            players.Clear();
            Changed = null;
        }
    }
}