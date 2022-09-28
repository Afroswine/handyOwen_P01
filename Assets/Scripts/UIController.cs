using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] Text _treasureText;
    [SerializeField] Text _healthText;

    private void Awake()
    {
        _player.m_TreasureUpdate.AddListener(UpdateTreasure);
        _player.m_HealthUpdate.AddListener(UpdateHealth);
        _player.m_PlayerDeath.AddListener(DeathScreen);
    }

    private void OnDisable()
    {
        _player.m_TreasureUpdate.RemoveListener(UpdateTreasure);
        _player.m_HealthUpdate.RemoveListener(UpdateHealth);
        _player.m_PlayerDeath.RemoveListener(DeathScreen);
    }

    private void Start()
    {
        UpdateHealth();
        UpdateTreasure();
    }

    private void UpdateHealth()
    {
        _healthText.text = "Health: " + _player.CurrentHealth;
    }

    private void UpdateTreasure()
    {
        _treasureText.text = "Treasure: " + _player.TreasureCount;
    }

    private void DeathScreen()
    {
        _player.m_TreasureUpdate.RemoveListener(UpdateTreasure);
        _player.m_HealthUpdate.RemoveListener(UpdateHealth);
        _player.m_PlayerDeath.RemoveListener(DeathScreen);
    }
}
