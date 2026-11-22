using System.Collections.Generic;
using UnityEngine;

namespace LostIsland.Items
{

    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "LostIsland/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        public const ushort None = 0;
        [SerializeField] private List<ItemDefinition> items = new List<ItemDefinition>();



        public ItemDefinition Get(ushort id)
        {
            if (id == None || id > items.Count) return null;
            return items[id - 1];
        }

        public ushort GetId(ItemDefinition item)
        {
            int index = items.IndexOf(item);
            return index < 0 ? None : (ushort)(index + 1);

        }


        private void OnValidate()
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i] != null && items.IndexOf(items[i]) != i)
                    Debug.LogWarning($"[ItemDatabase] Tekrarlanan eşya: {items[i].name}", this);


                    
        }
    }
}