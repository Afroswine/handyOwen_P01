using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackingState : State
{
    // this is our StateMachine owner
    BossController _bossController;

    // constructor lets us pass in references we need
    public BossAttackingState(BossController bossController)
    {
        _bossController = bossController;
    }

    public override void Enter()
    {
        Debug.Log("Boss Attacking Enter.");
    }

    public override void Update()
    {
        //Debug.Log("Boss Attacking Update.");
    }

    public override void Exit()
    {
        Debug.Log("Boss Attacking Exit.");
    }
}
