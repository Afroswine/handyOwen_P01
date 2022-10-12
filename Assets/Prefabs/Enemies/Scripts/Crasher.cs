using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider))]
public class Crasher : MonoBehaviour
{
    [SerializeField] BossMovement _movement;
    BoxCollider _collider;

    public event Action Crashed = delegate { };

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;

        DisableCollision();
    }

    private void OnEnable()
    {
        _movement.PathingEnded += DisableCollision;
        _movement.Charged += EnableCollision;
    }

    private void OnDisable()
    {
        _movement.PathingEnded -= DisableCollision;
        _movement.Charged -= EnableCollision;
    }

    public void EnableCollision()
    {
        _collider.enabled = true;
    }

    public void DisableCollision()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            DisableCollision();
            _movement.Crash();
        }
    }
}
