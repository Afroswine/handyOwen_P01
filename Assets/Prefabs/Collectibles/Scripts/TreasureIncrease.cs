using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureIncrease : CollectibleBase
{
    [Header("TreasureIncrease")]
    [SerializeField] int _treasureAmount = 1;

    protected override void Collect(Player player)
    {
        player.IncreaseTreasure(_treasureAmount);
        Destroy(gameObject);
    }

    protected override void Movement(Rigidbody rb)
    {
        Quaternion turnOffset = Quaternion.Euler
            (RotationSpeed, RotationSpeed, RotationSpeed);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
}
