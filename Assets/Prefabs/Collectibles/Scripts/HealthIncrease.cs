using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : CollectibleBase
{
    [Header("HealthIncrease")]
    [SerializeField] int _healthAdded = 1;
    
    protected override void Collect(Player player)
    {
        player.IncreaseHealth(_healthAdded);
        Destroy(gameObject);
    }
}
