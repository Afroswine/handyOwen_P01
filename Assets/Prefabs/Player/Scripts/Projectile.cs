using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    //[Tooltip("Projectile will only collide with these objects")]
    //[SerializeField] List<GameObject> _collidableObjects;
    //[Tooltip("Projectile will only collide with these entities")]
    [SerializeField] List<MonoBehaviour> _collidableScripts;
    [SerializeField] int _damage = 1;
    [SerializeField] float _projectileSpeed = 5f;
    [Tooltip("How long before the projectile expires.")]
    [SerializeField] float _lifetime = 20f;
    [Tooltip("Does the projectile ignore collisions with player?")]
    [SerializeField] bool _ignorePlayer = false;
    [Tooltip("Does the projectile ignore collisions with enemies?")]
    [SerializeField] bool _ignoreEnemies = false;
    [Tooltip("Does the projectile ignore collisions with projectiles?")]
    [SerializeField] bool _ignoreProjectiles = true;

    [Header("FX")]
    [Tooltip("PS to play upon collision.")]
    [SerializeField] ParticleSystem _hitPS;
    //ParticleSystem _currentHitPS;
    [Tooltip("Audio to play upon collision.")]
    [SerializeField] AudioClip _hitSound;
    [SerializeField] float _hitSoundVolume = 1f;

    private Rigidbody _rb;

    public void Fire(Vector3 direction)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(direction * _projectileSpeed, ForceMode.Force);

        StartCoroutine(DestroyCR());
    }

    // 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamageableEnemy>(out IDamageableEnemy enemy))
        {
            // if false, cause the enemy to take damage.
            if (!_ignoreEnemies)
            {
                enemy.TakeDamage(_damage);
                OnHit();
            }
        }
        else if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            // if false, cause the player to take damage
            if (!_ignorePlayer)
            {
                player.TakeDamage(_damage);
                OnHit();
            }
        }
        else if (other.gameObject.TryGetComponent<Projectile>(out Projectile projectile))
        {
            // if false, destroy both projectiles
            if (!_ignoreProjectiles)
            {
                projectile.OnHit();
                OnHit();
            }
        }
        else
        {
            OnHit();
        }

    }

    private void OnHit()
    {
        PSManager.Instance.SpawnPS(_hitPS, transform.position);
        AudioHelper.PlayClip2D(_hitSound, _hitSoundVolume);
        Destroy(gameObject);
    }

    private IEnumerator DestroyCR()
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }
}
