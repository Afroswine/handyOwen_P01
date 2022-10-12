using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPathingState : State
{
    // this is our StateMachine owner
    BossController _controller;
    BossMovement _movement;

    public Vector3 Destination; // used by BossMovementEditor

    // constructor lets us pass in references we need
    public BossPathingState(BossController bossController)
    {
        _controller = bossController;
        _movement = _controller.Movement;
    }

    public override void Enter()
    {
        //DetermineMovementAction();
        Charge(_controller.ChargeWindupDuration);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }

    // Move to a random position within radius of _controller.Target
    private void Move(float radius)
    {
        Vector3 destination = NavMeshUtility.RandomNavMeshLocation(radius, _controller.Target);
        Destination = destination;
        _movement.MoveToDestination(destination);
    }

    // Begin winding up for a fast charge movement
    private void Charge(float windupDuration)
    {
        _movement.BeginCharge(windupDuration);
    }

    // Determine whether to Move or Charge
    private void DetermineMovementAction()
    {
        // if the target is not far enough away to charge
        if (_controller.DistanceFromTarget(_controller.Target) < _controller.MinChargeDistance)
        {
            Move(_controller.MoveRadius);
        }
        else
        {
            Charge(_controller.ChargeWindupDuration);
        }
    }
}
