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
    [SerializeField] bool _ignorePlayer = false;
    [SerializeField] bool _ignoreEnemies = false;

    [Header("FX")]
    [SerializeField] ParticleSystem _hitPS;
    ParticleSystem _currentHitPS;
    [SerializeField] AudioClip _hitSound;

    private Rigidbody _rb;

    public void Fire(Vector3 direction)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(direction * _projectileSpeed, ForceMode.Force);

        StartCoroutine(DestroyCR());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IEnemyHealth>(out IEnemyHealth enemy))
        {
            if (!_ignoreEnemies)
            {
                enemy.TakeDamage(_damage);
                OnHit();
            }
        }
        else if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (!_ignorePlayer)
            {
                player.TakeDamage(_damage);
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
        _currentHitPS = Instantiate(_hitPS);
        _currentHitPS.transform.position = gameObject.transform.position;
        _currentHitPS.Play();
        AudioHelper.PlayClip2D(_hitSound, 1f);

        Destroy(gameObject);
    }

    private IEnumerator DestroyCR()
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }
}
