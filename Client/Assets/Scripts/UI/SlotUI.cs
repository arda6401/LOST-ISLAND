using System.Runtime.Serialization;
using LostIsland.Combat;
using LostIsland.Core;
using LostIsland.Player;
using LostIsland.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] public int SlotIndex = 1;
    // Todo OnSelectedAnim
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI AmmoText;
    [SerializeField] private Sprite SlotSprite;
    [SerializeField] private Sprite ActiveSlotSprite;
    [SerializeField] private Image SlotBG;
    private PlayerInventory boundInventory;
    private byte activeSlot;
    private bool playerSpawned;

    void Awake()
    {
        Clear();
        HideSlotBackground();
    }

    private void OnEnable()
    {
        LocalPlayerEvents.Spawned += OnSpawnPlayer;
        LocalPlayerEvents.Despawned += OnPlayerDespawned;

        if (LocalPlayerEvents.Current != null)
            OnSpawnPlayer(LocalPlayerEvents.Current);
    }
    private void OnDisable()
    {
        LocalPlayerEvents.Spawned -= OnSpawnPlayer;
        LocalPlayerEvents.Despawned -= OnPlayerDespawned;
        UnbindInventory();
        playerSpawned = false;
        HideSlotBackground();
    }
    public void Set(WeaponDefinition weapon, int ammo)
    {
        if (weapon == null)
        {
            Clear();
            return;
        }

        Icon.sprite = weapon.Icon;
        Icon.gameObject.SetActive(true);
        AmmoText.gameObject.SetActive(true);
        AmmoText.text = ammo.ToString();

        if (SlotBG != null)
        {
            SlotBG.gameObject.SetActive(true);
            UpdateSlotBackground();
        }
    }
    private void OnSpawnPlayer(Transform player)
    {
        UnbindInventory();
        playerSpawned = true;

        if (player != null)
            boundInventory = player.GetComponent<PlayerInventory>();

        if (boundInventory != null)
        {
            boundInventory.ActiveSlotChanged += OnActiveSlotChanged;
            OnActiveSlotChanged(boundInventory.ActiveSlot);
        }
        else
        {
            UpdateSlotBackground();
        }
    }

    private void OnPlayerDespawned()
    {
        playerSpawned = false;
        UnbindInventory();
        HideSlotBackground();
    }

    private void UnbindInventory()
    {
        if (boundInventory != null)
            boundInventory.ActiveSlotChanged -= OnActiveSlotChanged;

        boundInventory = null;
    }

    private void OnActiveSlotChanged(byte activeslot)
    {
        activeSlot = activeslot;
        UpdateSlotBackground();
    }

    private void UpdateSlotBackground()
    {
        if (SlotBG == null) return;
        if (!playerSpawned) return;

        SlotBG.gameObject.SetActive(true);

        if ((SlotIndex - 1) == activeSlot)
            SlotBG.sprite = ActiveSlotSprite;
        else
            SlotBG.sprite = SlotSprite;
    }

    public void Clear()
    {
        if (Icon != null)
        {
            Icon.sprite = null;
            Icon.gameObject.SetActive(false);
        }
        if (AmmoText != null)
        {
            AmmoText.text = "";
            AmmoText.gameObject.SetActive(false);
        }

        if (playerSpawned)
            UpdateSlotBackground();
    }

    private void HideSlotBackground()
    {
        if (SlotBG != null)
            SlotBG.gameObject.SetActive(false);
    }

    public void RequestChangeSlot() => boundInventory.RequestSwitch();

}
