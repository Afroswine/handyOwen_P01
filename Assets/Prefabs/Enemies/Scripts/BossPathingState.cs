using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPathingState : State
{
    // this is our StateMachine owner
    BossController _controller;
    BossMovement _movement;

    // charge is taken off cooldown after performing a normal movement action
    private bool _chargeOnCooldown = true;

    // constructor lets us pass in references we need
    public BossPathingState(BossController bossController)
    {
        _controller = bossController;
        _movement = _controller.Movement;
    }

    public override void Enter()
    {
        Move(_controller.MoveRadius);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _controller.ChargeOnCooldown = false;
    }

    // Move to a random position within radius of _controller.Target
    private void Move(float radius)
    {
        Vector3 destination = NavMeshUtility.RandomNavMeshLocation(radius, _controller.Target);
        _movement.MoveToDestination(destination);
        _chargeOnCooldown = false;
    }
}
