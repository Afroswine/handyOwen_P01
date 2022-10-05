using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int _maxHealth = 5;
    public int MaxHealth => _maxHealth;
    private int _currentHealth;
    public int CurrentHealth => _currentHealth;

    public event Action TookDamage;
    public event Action Killed;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        TookDamage.Invoke();
        if (_currentHealth <= 0)
        {
            Kill();
        }
    }
    
    public void Kill()
    {
        _currentHealth = 0;
        Killed.Invoke();
    }

}


