using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrease : CollectibleBase
{
    [Header("SpeedIncrease")]
    [SerializeField] float _speedMultiplier = 2f;
    [SerializeField] float _duration = 3f;

    protected override void Collect(Player player)
    {
        //pull motor controller from the player
        TankController controller = player.GetComponent<TankController>();
        if(controller != null)
        {
            controller.MultiplySpeed(_speedMultiplier, _duration);
            Destroy(gameObject);
        }
    }

    protected override void Movement(Rigidbody rb)
    {
        Quaternion turnOffset = Quaternion.Euler
            (RotationSpeed, RotationSpeed, RotationSpeed);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
}
