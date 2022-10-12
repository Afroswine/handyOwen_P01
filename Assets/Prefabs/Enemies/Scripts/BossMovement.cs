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
    public event Action Charged = delegate { };
    

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

    private void ResetAgentSteering()
    {
        _agent.speed = _originalSpeed;
        _agent.angularSpeed = _originalAngularSpeed;
        _agent.acceleration = _originalAcceleration;
        _agent.autoBraking = true;
    }
    
    // set agent steering stats
    private void SetAgentSteering(float speed, float angularSpeed, float acceleration, bool autoBraking)
    {
        _agent.speed = speed;
        _agent.angularSpeed = angularSpeed;
        _agent.acceleration = acceleration;
        _agent.autoBraking = autoBraking;
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

        // Charge after delay
        IEnumerator ChargeWindupCR(float delay)
        {
            _reachedDestination = false;
            SetAgentSteering(0f, _originalAngularSpeed, _originalAcceleration, true);

            float timestamp = Time.time + delay;
            NavMeshHit hit;
            Vector3 destination = default(Vector3);

            // Windup Phase, rotate towards target every frame
            while (Time.time < timestamp)
            {
                ChargeWindupRotation();

                _agent.Raycast(_controller.transform.forward * 100f, out hit);
                destination = hit.position;
                Destination = destination;

                _agent.SetDestination(destination);
                yield return new WaitForEndOfFrame();
            }

            // Charge forward
            Charge(destination);

        }

        // Rotate towards the player during windup
        void ChargeWindupRotation()
        {
            Vector3 aimPos = _controller.Target.transform.position - _controller.transform.position;
            aimPos.y = 0;
            Quaternion goalRotation = Quaternion.LookRotation(aimPos, Vector3.up);

            _controller.transform.rotation = Quaternion.Slerp(_controller.transform.rotation, goalRotation, Time.deltaTime * 2f);
        }

        // Charge forward
        void Charge(Vector3 destination)
        {
            SetAgentSteering(_controller.ChargeSpeed, _controller.ChargeAngularSpeed, _controller.ChargeAcceleration, false);
            MoveToDestination(destination);
            Charged.Invoke();
            StartCoroutine(DestinationStatusCR());
        }
    }

    public void Crash()
    {
        _agent.isStopped = true;
        _agent.ResetPath();
    }

    // Checks every frame to see if the agent has reached its destination
    private IEnumerator DestinationStatusCR()
    {
        while(_reachedDestination == false)
        {
            _reachedDestination = NavMeshUtility.ReachedDestinationOrGaveUp(_agent);
            yield return new WaitForEndOfFrame();
        }
        
        Debug.Log("Boss has reached its destination.");
        ResetAgentSteering();
        PathingEnded.Invoke();
    }
    
}