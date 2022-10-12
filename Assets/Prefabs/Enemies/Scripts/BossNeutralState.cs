using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNeutralState : State
{
    // this is our StateMachine owner
    BossController _controller;

    // constructor lets us pass in references we need
    public BossNeutralState(BossController bossController)
    {
        _controller = bossController;
    }

    public override void Enter()
    {
        //Debug.Log("Boss Neutral Enter.");
        _controller.ChangeStateDelayed(_controller.PathingState, _controller.MoveWait);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        //Debug.Log("Boss Neutral Exit.");
    }
}
