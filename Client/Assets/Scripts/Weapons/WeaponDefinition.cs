using UnityEngine;



namespace LostIsland.Weapons
{

    [CreateAssetMenu(fileName = "Weapon_", menuName = "LostIsland/Weapon Definition")]
    public class WeaponDefinition : ScriptableObject
    {
        [Header("Kimlik")]
        public string displayName = "Saldırı Tüfeği";

        [Header("Atış")]
        [Tooltip("İki atış arası süre (sn). 0.15 = saniyede ~6,7 atış")]
        public float fireInterval = 0.15f;

        [Header("Mermi")]
        [Tooltip("Mermi hızı (m/sn)")]
        public float projectileSpeed = 40f;

        [Tooltip("Çarpışma küresinin yarıçapı (m)")]
        public float projectileRadius = 0.1f;

        [Tooltip("Isabet alan mesafe (metre)")]
        public float range = 15f;

        [Tooltip("Tek mermi hasarı")]
        public int damage = 10;
    }
}