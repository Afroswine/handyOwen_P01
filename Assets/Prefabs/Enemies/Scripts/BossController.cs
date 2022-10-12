using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BossController : StateMachine
{
    [Header("Boss Controller")]
    [Header("Targeting")]
    [SerializeField] GameObject _target;
    [Header("Movement")]
    [SerializeField] BossMovement _movementScript;
    [Tooltip("Wait time between normal movement actions.")]
    [SerializeField] float _moveWait = 2f;
    [Tooltip("Max distance to check for normal movement destinations.")]
    [SerializeField] float _moveRadius = 20f;
    [Header("Charge Movement")]
    [SerializeField] BoxCollider _crashCollider;
    [Tooltip("Min distance the boss has to be from the player to charge at them.")]
    [SerializeField] float _minChargeDistance = 20f;
    [SerializeField] float _chargeWindupDuration = 3f;
    [SerializeField] float _chargeSpeed = 10f;
    [SerializeField] float _chargeAngularSpeed = 60f;
    [SerializeField] float _chargeAcceleration = 16f;

    #region Public Reference Variables
    public NavMeshAgent Agent { get; private set; }
    public GameObject Target => _target;
    public BossMovement Movement => _movementScript;

    // Movement
    public float MoveWait => _moveWait;
    public float MoveRadius => _moveRadius;

    // Charge
    public BoxCollider CrashCollider => _crashCollider;
    public float ChargeWindupDuration => _chargeWindupDuration;
    public float MinChargeDistance => _minChargeDistance;
    public float ChargeSpeed => _chargeSpeed;
    public float ChargeAngularSpeed => _chargeAngularSpeed;
    public float ChargeAcceleration => _chargeAcceleration;
    public bool ChargeOnCooldown = true;
    #endregion Public Reference Variables END

    #region Boss States
    public BossNeutralState NeutralState;
    public BossPathingState PathingState;
    public BossChargingState ChargingState;
    #endregion Boss States END

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();

        // initialize all states
        NeutralState = new BossNeutralState(this);
        PathingState = new BossPathingState(this);
        ChargingState = new BossChargingState(this);

        //Movement = new BossMovement(this);
    }

    private void OnEnable()
    {
        Movement.PathingEnded += OnPathingEnded;
    }

    private void OnDisable()
    {
        Movement.PathingEnded -= OnPathingEnded;
    }

    // Start is called before the first frame update
    void Start()
    {
        BeginState(NeutralState);
    }

    void OnPathingEnded()
    {
        ChangeState(NeutralState);
    }

    public void ChangeStateDelayed(State state, float delay)
    {
        StartCoroutine(ChangeStateDelayedCR(state, delay));
    }

    private IEnumerator ChangeStateDelayedCR(State state, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(state);
    }

    public float DistanceFromTarget(GameObject go)
    {
        float distance = Vector3.Distance(gameObject.transform.position, go.transform.position);
        return distance;
    }
}
