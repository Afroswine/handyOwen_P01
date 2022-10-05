using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int MaxHealth { get;}
    int CurrentHealth { get; }

    void TakeDamage(int damage);
}

public interface IDamageablePlayer : IDamageable
{

}

public interface IDamageableEnemy : IDamageable
{
    
}
