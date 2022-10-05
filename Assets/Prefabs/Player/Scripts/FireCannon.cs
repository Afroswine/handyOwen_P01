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
    [SerializeField] float _fireSoundVolume = 1f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        if (!_isOnCooldown)
        {
            // Create projectile and launch it
            _currentProjectile = Instantiate(_projectile);
            _currentProjectile.transform.position = _origin.position;
            _currentProjectile.transform.forward = _origin.forward;
            _currentProjectile.Fire(_currentProjectile.transform.forward);

            PSManager.Instance.SpawnPS(_firePS,_origin.position, _origin.rotation);
            AudioHelper.PlayClip2D(_fireSound, _fireSoundVolume);

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
