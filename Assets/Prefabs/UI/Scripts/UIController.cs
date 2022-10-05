using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //[SerializeField] private Player _player;
    [SerializeField] private Health _playerHealth;
    [SerializeField] Text _treasureText;
    [SerializeField] Text _healthText;


    private void Awake()
    {

    }

    private void OnEnable()
    {
        _playerHealth.TookDamage += UpdateHealth;
        _playerHealth.Killed += DeathScreen;
    }

    private void OnDisable()
    {
        _playerHealth.TookDamage -= UpdateHealth;
        _playerHealth.Killed -= DeathScreen;
    }

    private void Start()
    {
        UpdateHealth();
        UpdateTreasure();
    }

    private void UpdateHealth()
    {
        //_healthText.text = "Health: " + _player.CurrentHealth;
        _healthText.text = "Health: " + _playerHealth.CurrentHealth;
    }

    private void UpdateTreasure()
    {
        //_treasureText.text = "Treasure: " + _player.TreasureCount;
    }

    private void DeathScreen()
    {
        //_player.m_TreasureUpdate.RemoveListener(UpdateTreasure);
        //_player.m_HealthUpdate.RemoveListener(UpdateHealth);
        //_player.m_PlayerDeath.RemoveListener(DeathScreen);
    }
}
