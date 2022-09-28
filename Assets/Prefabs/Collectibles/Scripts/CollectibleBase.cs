using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CollectibleBase : MonoBehaviour
{
    protected abstract void Collect(Player player);

    [Header("CollectibleBase")]
    [Tooltip("Requires a Rigidbody and MeshRenderer.")]
    [SerializeField] GameObject _art;
    [SerializeField] float _rotationSpeed = 1;
    protected float RotationSpeed => _rotationSpeed;
    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = _art.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler(0, _rotationSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            Collect(player);
            Feedback();

            gameObject.SetActive(false);
        }
    }

    private void Feedback()
    {
        // particles
        if(_collectParticles != null)
        {
            _collectParticles = Instantiate(_collectParticles,
                transform.position, Quaternion.identity);
            _collectParticles.Play();
        }
        //audio TODO - consider Object Pooling
        if(_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 1f);
        }
    }
}
