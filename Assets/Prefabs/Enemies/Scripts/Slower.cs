using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Slower : Enemy
{
    [Header("Slower")]
    [SerializeField] float _speedMultiplier = 0.5f;
    [SerializeField] float _duration = 3f;

    protected override void PlayerImpact(Player player)
    {
        base.PlayerImpact(player);

        TankController controller = player.GetComponent<TankController>();
        if(controller != null)
        {
            controller.MultiplySpeed(_speedMultiplier, _duration);
        }
    }
}
