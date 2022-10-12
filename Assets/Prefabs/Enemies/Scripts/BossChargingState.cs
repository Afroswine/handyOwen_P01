using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargingState : State
{
    // this is our StateMachine owner
    BossController _controller;
    BossMovement _movement;
    BoxCollider _crashCollider;

    // constructor lets us pass in references we need
    public BossChargingState(BossController bossController)
    {
        _controller = bossController;
        _movement = _controller.Movement;
        _crashCollider = _controller.CrashCollider;
    }

    public override void Enter()
    {
        Charge(_controller.ChargeWindupDuration);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        _controller.ChargeOnCooldown = true;
    }

    // Begin winding up for a fast charge movement
    private void Charge(float windupDuration)
    {
        _movement.BeginCharge(windupDuration);
    }
}
