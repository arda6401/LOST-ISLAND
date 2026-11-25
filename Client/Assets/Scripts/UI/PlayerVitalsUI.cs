using System;
using LostIsland.Combat;
using LostIsland.Core;
using LostIsland.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVitalsUI : MonoBehaviour
{
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider ShieldSlider;
    [SerializeField] private TextMeshProUGUI HealthText;
    [SerializeField] private TextMeshProUGUI ShieldText;


    private Health health;

    private void OnEnable()
    {
        LocalPlayerEvents.Spawned += OnPlayerSpawned;
        LocalPlayerEvents.Despawned += OnPlayerDespawned;


        if (LocalPlayerEvents.Current != null)
            Bind(LocalPlayerEvents.Current);
    }

    private void OnDisable()
    {
        LocalPlayerEvents.Spawned -= OnPlayerSpawned;
        LocalPlayerEvents.Despawned -= OnPlayerDespawned;
    }


    private void OnPlayerSpawned(Transform player)
    {
        Bind(player);
    }
    private void OnPlayerDespawned()
    {
        Unbind();
    }


    private void OnUpdateHealth(int current, int max)
    {
        HealthText.text = current.ToString();
        HealthSlider.value = max > 0 ? (float)current / max : 0f;
    }



    private void Bind(Transform player)
    {
        Unbind();
        health = player.GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError("[PLAYER VİTALS] health is null...");
            return;
        }

        health.HealthChanged += OnUpdateHealth;
        OnUpdateHealth(health.Current, health.Max);


    }
    private void Unbind()
    {
        if (health == null) return;

        health.HealthChanged -= OnUpdateHealth;
        health = null;

    }


}
