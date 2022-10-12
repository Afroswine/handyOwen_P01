using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class BossMovement : MonoBehaviour
{
    public Vector3 Destination { get; private set; }    // Used by BossMovementEditor

    BossController _controller;
    NavMeshAgent _agent;    // The agent we are moving
    GameObject _target;
    bool _reachedDestination = false;

    float _originalSpeed;
    float _originalAngularSpeed;
    float _originalAcceleration;

    // events
    public event Action PathingEnded = delegate { };

    /*
    public BossMovement(BossController controller)
    {
        _controller = controller;
        _agent = _controller.Agent;
    }
    */

    private void Start()
    {
        _controller = GetComponent<BossController>();
        _agent = _controller.Agent;
        _target = _controller.Target;

        // save original steering stats
        _originalSpeed = _agent.speed;
        _originalAngularSpeed = _agent.angularSpeed;
        _originalAcceleration = _agent.acceleration;
    }

    // set agent steering stats
    private void SetAgentSteering(float speed, float angularSpeed, float acceleration)
    {
        _agent.speed = speed;
        _agent.angularSpeed = angularSpeed;
        _agent.acceleration = acceleration;
    }

    // Tells the agent to move to the given destination
    public void MoveToDestination(Vector3 destination)
    {
        _reachedDestination = false;
        Destination = destination;
        _agent.SetDestination(destination);
        StartCoroutine(DestinationStatusCR());
    }

    // Reduce speed charge after "duration"
    public void BeginCharge(float duration)
    {
        StartCoroutine(ChargeWindupCR(duration));
    }

    // Charge after delay
    private IEnumerator ChargeWindupCR(float delay)
    {
        SetAgentSteering(0f, _originalAngularSpeed, _originalAcceleration);

        float timestamp = Time.time + delay;
        NavMeshHit hit;

        while(Time.time < timestamp)
        {
            _agent.Raycast(_controller.transform.forward * 100f, out hit);
            Destination = hit.position;
            _agent.SetDestination(hit.position);
            yield return new WaitForEndOfFrame();
        }

        Charge(Destination);
    }
    
    // Charge directly at the target with temporary speed and acceleration adjustments
    private void Charge(Vector3 destination)
    {
        SetAgentSteering(_controller.ChargeSpeed, _controller.ChargeAngularSpeed, _controller.ChargeAcceleration);
        MoveToDestination(destination * 1.15f);
        StartCoroutine(DestinationStatusCR(true));
    }

    // Checks every frame to see if the agent has reached its destination
    private IEnumerator DestinationStatusCR(bool resetSteering = false)
    {
        while(_reachedDestination == false)
        {
            _reachedDestination = NavMeshUtility.ReachedDestinationOrGaveUp(_agent);
            yield return new WaitForEndOfFrame();
        }
        if (resetSteering)
        {
            SetAgentSteering(_originalSpeed, _originalAngularSpeed, _originalAcceleration);
        }

        PathingEnded.Invoke();
        Debug.Log("Boss has reached its destination.");
    }
    
}