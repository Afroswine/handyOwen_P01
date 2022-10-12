using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNeutralState : State
{
    // this is our StateMachine owner
    BossController _controller;
    BossMovement _movement;

    // constructor lets us pass in references we need
    public BossNeutralState(BossController bossController)
    {
        _controller = bossController;
    }

    public override void Enter()
    {
        DetermineNextState();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }

    private void DetermineNextState()
    {
        if (!_controller.ChargeOnCooldown)
        {
            // Charge
            if(_controller.DistanceFromTarget(_controller.Target) >= _controller.MinChargeDistance)
            {
                _controller.ChangeStateDelayed(_controller.ChargingState, _controller.MoveWait);
                return;
            }
            // Move
            else
            {
                _controller.ChangeStateDelayed(_controller.PathingState, _controller.MoveWait);
                return;
            }
        }

        // Move
        if (_controller.ChargeOnCooldown)
        {
            _controller.ChangeStateDelayed(_controller.PathingState, _controller.MoveWait);
            return;
        }
    }
}
