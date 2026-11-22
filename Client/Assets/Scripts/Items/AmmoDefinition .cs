using UnityEngine;

namespace LostIsland.Items
{

    [CreateAssetMenu(fileName = "Ammo_", menuName = "LostIsland/Ammo Definition")]
    public class AmmoDefinition : ItemDefinition
    {
        private void Reset() => maxStack = 90;
    }
}