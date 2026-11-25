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

    void Awake()
    {
        Clear();
    }

    public void Set(WeaponDefinition weapon,int ammo)
    {
        if (weapon == null)
        {
            Clear();
            return;
        }

        Icon.sprite = weapon.Icon;
        Icon.gameObject.SetActive(true);
        AmmoText.gameObject.SetActive(true);
        AmmoText.text =  ammo.ToString();
    }

    public void Clear()
    {
        if (Icon == null) return;

        Icon.sprite = null;
        Icon.gameObject.SetActive(false);
         if (AmmoText != null)
    {
        AmmoText.text = "";
        AmmoText.gameObject.SetActive(false);
    }
    }
}
