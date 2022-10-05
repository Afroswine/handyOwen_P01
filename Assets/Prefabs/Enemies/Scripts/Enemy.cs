using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour, IDamageableEnemy
{
    [Header("IHealth")]
    [SerializeField] int _maxHealth;
    public int MaxHealth => _maxHealth;
    [SerializeField] int _currentHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        private set { _currentHealth = value; }
    }
    [Header("Enemy")]
    [SerializeField] int _damageAmount = 1;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Lose health upon taking damage
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }

    public void Move()
    {

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
        player.TakeDamage(_damageAmount);
    }

    // FX to play upon colliding with player
    private void ImpactFeedback()
    {
        //particles
        if (_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles,
                transform.position, Quaternion.identity);
            PSManager.Instance.SpawnPS(_impactParticles, transform.position);
        }
        //audio TODO - consider Object pooling for performance
        if (_impactSound != null)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
        }
    }
}
