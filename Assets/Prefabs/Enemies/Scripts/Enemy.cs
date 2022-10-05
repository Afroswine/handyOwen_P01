using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] int _touchDamage = 1;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;
    [SerializeField] float _impactSoundVolume = 1f;
    private Health _health;

    Rigidbody _rb;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _health.TookDamage += TakeDamage;
        _health.Killed += Kill;
    }

    private void OnDisable()
    {
        _health.TookDamage -= TakeDamage;
        _health.Killed -= Kill;
    }

    private void FixedUpdate()
    {
        //Move();
    }

    public void Move()
    {

    }

    public void TakeDamage()
    {

    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    // If colliding with player, call PlayerImpact() and ImpactFeedback()
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PlayerImpact(player);
            ImpactFeedback();
        }
    }

    // Actions to perform upon colliding with player
    protected virtual void PlayerImpact(Player player)
    {
        if(player.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(_touchDamage);
        }
    }

    // FX to play upon colliding with player
    private void ImpactFeedback()
    {
        //particles
        if (_impactParticles != null)
        {
            //PSManager.Instance.SpawnPS(_impactParticles, transform.position);
        }
        PSManager.Instance.SpawnPS(_impactParticles, transform.position);
        //audio TODO - consider Object pooling for performance
        if (_impactSound != null)
        {
            AudioHelper.PlayClip2D(_impactSound, _impactSoundVolume);
        }
    }
}
