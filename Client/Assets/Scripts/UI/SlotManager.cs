using LostIsland.Combat;
using LostIsland.Core;
using LostIsland.Items;
using LostIsland.Weapons;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private SlotUI[] slotUIs;
    [SerializeField] private ItemDatabase database;

    private PlayerInventory inventory;

    private void Start()
    {
        LocalPlayerEvents.Spawned += OnLocalPlayerSpawned;
        LocalPlayerEvents.Despawned += OnLocalPlayerDespawned;

        // Yerel oyuncu UI'dan once spawn olduysa kacirmayalim.
        if (LocalPlayerEvents.Current != null)
            Bind(LocalPlayerEvents.Current);
    }

    private void OnDestroy()
    {
        LocalPlayerEvents.Spawned -= OnLocalPlayerSpawned;
        LocalPlayerEvents.Despawned -= OnLocalPlayerDespawned;
        Unbind();
    }

    private void OnLocalPlayerSpawned(Transform player) => Bind(player);

    private void OnLocalPlayerDespawned() => Unbind();

    private void Bind(Transform player)
    {
        Unbind();
        if (player == null) return;

        inventory = player.GetComponent<PlayerInventory>();
        if (inventory == null) return;

        inventory.SlotChanged += OnSlotChanged;
        inventory.SlotsCleared += ClearAll;

        // Ilk senkronizasyon UI baglanmadan once gelmis olabilir, mevcut hali cizelim.
        for (int i = 0; i < inventory.Count; i++)
            OnSlotChanged(i, inventory.GetSlot(i));
    }

    private void Unbind()
    {
        if (inventory != null)
        {
            inventory.SlotChanged -= OnSlotChanged;
            inventory.SlotsCleared -= ClearAll;
            inventory = null;
        }

        ClearAll();
    }

    private void OnSlotChanged(int index, WeaponSlot slot)
    {
        SlotUI slotUI = GetSlotUI(index);
        if (slotUI == null) return;

        if (slot.weaponId != 0
            && database != null
            && database.TryGetWeapon(slot.weaponId, out WeaponDefinition weapon))
        {
            slotUI.Set(weapon,slot.ammo);
        }
        else
        {
            slotUI.Clear();
        }
    }

    // SlotUI.SlotIndex sahnede 1 tabanli tutuluyor.
    private SlotUI GetSlotUI(int index)
    {
        foreach (SlotUI slotUI in slotUIs)
            if (slotUI != null && slotUI.SlotIndex == index + 1)
                return slotUI;

        return null;
    }

    private void ClearAll()
    {
        foreach (SlotUI slotUI in slotUIs)
            if (slotUI != null) slotUI.Clear();
    }
}
