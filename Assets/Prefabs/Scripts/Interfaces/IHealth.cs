using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    int MaxHealth { get;}
    int CurrentHealth { get; }

    void TakeDamage(int damage);
}

public interface IEnemyHealth : IHealth
{
    
}
