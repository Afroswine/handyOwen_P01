using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    [Header("Fire Cannon")]
    [SerializeField] Projectile _projectile;
    private Projectile _currentProjectile;
    [SerializeField] Transform _origin;
    [SerializeField] float _cooldown;
    private bool _isOnCooldown = false;
    [Header("FX")]
    [SerializeField] ParticleSystem _firePS;
    ParticleSystem _currentFirePS;
    [SerializeField] AudioClip _fireSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }
    }

    void SpawnProjectile()
    {
        if (!_isOnCooldown)
        {
            // Create projectile and launch it
            _currentProjectile = Instantiate(_projectile);
            _currentProjectile.transform.position = _origin.position;
            _currentProjectile.transform.forward = _origin.forward;
            _currentProjectile.Fire(_currentProjectile.transform.forward);

            _currentFirePS = Instantiate(_firePS);
            _currentFirePS.transform.position = _origin.position;
            _currentFirePS.transform.forward = _origin.forward;
            _currentFirePS.Play();

            AudioHelper.PlayClip2D(_fireSound, 1f);

            //Go on cooldown
            StartCoroutine(CooldownCR());
        }
    }

    private IEnumerator CooldownCR()
    {
        _isOnCooldown = true;
        yield return new WaitForSeconds(_cooldown);
        _isOnCooldown = false;
    }
}
