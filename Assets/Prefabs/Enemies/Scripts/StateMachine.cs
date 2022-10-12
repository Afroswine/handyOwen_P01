using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    // Update is called once per frame
    private void Update()
    {
        CurrentState.Update();
    }

    public virtual void BeginState(State state)
    {
        CurrentState = state;
        state.Enter();
        Debug.Log("Begin State: " + state.ToString());
    }

    public virtual void ChangeState(State newState)
    {
        Debug.Log("Changed State: " + CurrentState.ToString() + " => " + newState.ToString());
        // run Exit() on current state
        CurrentState.Exit();

        CurrentState = newState;
        // run Enter() on new state
        newState.Enter();
    }
}
