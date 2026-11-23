using UnityEngine;

namespace LostIsland.Items
{
    public abstract class ItemDefinition : ScriptableObject
    {
        [Header("Eşya")]
        public string displayName = "Eşya";
        public Sprite Icon;

        [Tooltip("Envanterde bir yuvada en fazla kaç tane durur")]
        public int maxStack = 1;

        [Header("Toplama")]
        [Tooltip("Alanda kesintisiz durulması gereken süre (sn)")]
        public float pickupTime = 0.5f;

        [Header("Geçici görsel")]
        public GameObject ItemPrefab;
    }
}