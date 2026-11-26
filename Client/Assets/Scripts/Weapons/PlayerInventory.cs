using System;
using LostIsland.Items;
using LostIsland.Weapons;
using Mirror;
using UnityEngine;

namespace LostIsland.Combat
{
    public class PlayerInventory : NetworkBehaviour
    {
        public const int SlotCount = 2;

        [SerializeField] private ItemDatabase database;

        // WeaponSlot bir struct oldugu icin slots[i] = slot atamasi OP_SET
        // olarak kaydedilir ve istemcilere senkronlanir.
        private readonly SyncList<WeaponSlot> slots = new SyncList<WeaponSlot>();

        [SyncVar(hook = nameof(OnActiveSlotChanged))]
        private byte activeSlot = 0;

        public int Count => slots.Count;
        public byte ActiveSlot => activeSlot;

        public WeaponSlot GetSlot(int index) => slots[index];

        public bool TryGetActiveSlot(out WeaponSlot slot)
        {
            slot = default;
            if (activeSlot >= slots.Count) return false;

            slot = slots[activeSlot];
            return true;
        }

        private void OnActiveSlotChanged(byte oldValue, byte newValue)
        {
            ActiveSlotChanged?.Invoke(newValue);
        }



        public override void OnStartServer()
        {
            slots.Clear();
            for (int i = 0; i < SlotCount; i++)
                slots.Add(default);
        }

        [Server]
        public bool TryConsumeAmmo()
        {
            if (!TryGetActiveSlot(out WeaponSlot slot)) return false;
            if (slot.ammo <= 0) return false;

            slot.ammo--;
            slots[activeSlot] = slot;
            return true;
        }


        public bool TryGetActiveSlotWeapon(out WeaponDefinition weapon)
        {
            weapon = null;
            if (!TryGetActiveSlot(out WeaponSlot slot)) return false;

            return database.TryGetWeapon(slot.weaponId, out weapon);
        }


        [Server]
        public bool TryPickup(ushort weaponId)
        {
            if (database == null || !database.TryGetWeapon(weaponId, out WeaponDefinition weapon))
                return false;

            int target = -1;

            // Once aktif slot, sonra ilk bos slot, hepsi doluysa aktif slot degisir.
            if (activeSlot < this.slots.Count && this.slots[activeSlot].weaponId == 0)
            {
                target = activeSlot;
            }
            else
            {
                for (int i = 0; i < this.slots.Count; i++)
                {
                    if (this.slots[i].weaponId != 0) continue;

                    target = i;
                    break;
                }
            }

            if (target < 0 && slots.Count > 0)
                target = Mathf.Min(activeSlot, slots.Count - 1);

            if (target < 0) return false;

            WeaponSlot slot = this.slots[target];
            slot.weaponId = weaponId;
            slot.ammo = weapon.magazineSize;
            this.slots[target] = slot;

            return true;
        }

        #region Client

        public event Action<byte> ActiveSlotChanged;
        public event Action<int, WeaponSlot> SlotChanged;
        public event Action SlotsCleared;

        public override void OnStartClient()
        {
            slots.Callback += OnSlotsChanged;
        }

        public override void OnStopClient()
        {
            slots.Callback -= OnSlotsChanged;
        }

        // Dikkat: SyncList.OnChange OP_SET icin ESKI degeri verir,
        // bu yuzden yeni degeri veren Callback kullaniliyor.
        private void OnSlotsChanged(SyncList<WeaponSlot>.Operation op, int index, WeaponSlot oldSlot, WeaponSlot newSlot)
        {
            switch (op)
            {
                case SyncList<WeaponSlot>.Operation.OP_ADD:
                case SyncList<WeaponSlot>.Operation.OP_INSERT:
                case SyncList<WeaponSlot>.Operation.OP_SET:
                    SlotChanged?.Invoke(index, newSlot);
                    break;
                case SyncList<WeaponSlot>.Operation.OP_REMOVEAT:
                    SlotChanged?.Invoke(index, default);
                    break;
                case SyncList<WeaponSlot>.Operation.OP_CLEAR:
                    SlotsCleared?.Invoke();
                    break;
            }
        }

        [Command]
        private void CmdSwitchWeapon()
        {
            if (slots.Count == 0) return;

            activeSlot = (byte)((activeSlot + 1) % slots.Count);
        }

        public bool TryGetWeaponById(ushort id, out WeaponDefinition weapon)
    => database.TryGetWeapon(id, out weapon);

        public void RequestSwitch() => CmdSwitchWeapon();

        #endregion
    }
}
