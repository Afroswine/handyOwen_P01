using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    // backing field
    [Header("Player")]
    [Tooltip("The painted material of the tank's body.")]
    [SerializeField] Material _bodyMaterial;
    public Material BodyMaterial => _bodyMaterial;
    [Tooltip("A list of the recolorable parts of the tank's body.")]
    [SerializeField] List<GameObject> _recolorableParts;

    [Header("Feedback")]
    [Tooltip("PS to play when the player is hurt.")]
    [SerializeField] ParticleSystem _hurtPS;
    [Tooltip("Audio to play when the player is hurt.")]
    [SerializeField] AudioClip _hurtSound;
    [SerializeField] float _hurtSoundVolume = 1f;
    [Tooltip("PS to play when the player is killed.")]
    [SerializeField] ParticleSystem _deathPS;
    [Tooltip("Audio to play when the player is killed.")]
    [SerializeField] AudioClip _deathSound;
    [SerializeField] float _deathSoundVolume = 1f;
    [Tooltip("PS to play when the player blocks an attack.")]
    [SerializeField] ParticleSystem _blockPS;
    [Tooltip("Audio to play when the player blocks an attack.")]
    [SerializeField] AudioClip _blockSound;
    [SerializeField] float _blockSoundVolume = 1f;
    
    [Header("States")]
    public bool IsInvincible = false;
    [SerializeField] float _invulernabilityDuration = 1.5f;

    int _treasureCount = 0;
    public int TreasureCount => _treasureCount;

    TankController _tankController;
    BoxCollider _collider;
    Health _health;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
        _collider = GetComponent<BoxCollider>();
        _health = GetComponent<Health>();
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


    public void TakeDamage()
    {
        // if the player isn't invincible
        if (!IsInvincible)
        {
            AudioHelper.PlayClip2D(_hurtSound, _hurtSoundVolume);
            PSManager.Instance.SpawnPS(_hurtPS, transform.position);
        }
        // if the player *is* invincible
        else
        {
            AudioHelper.PlayClip2D(_blockSound, _blockSoundVolume);
            PSManager.Instance.SpawnPS(_blockPS, transform.position);
        }
    }

    private IEnumerator InvulernabilityPeriodCR()
    {
        IsInvincible = true;
        //_collider.isTrigger = true;
        yield return new WaitForSeconds(_invulernabilityDuration);
        IsInvincible = false;
        //_collider.isTrigger = false;
    }
    
    public void IncreaseTreasure(int amount)
    {
        _treasureCount += amount;
    }

    // Recolors the list of _recolorableParts on the Tank
    public void Recolor(Material material)
    {
        // go through the list of _recolorableParts and apply new material
        foreach (GameObject go in _recolorableParts)
        {
            go.GetComponent<MeshRenderer>().material = material;
        }
    }

    // Kills the player, setting health to 0 and deactivating them
    public void Kill()
    {
        // FX
        AudioHelper.PlayClip2D(_deathSound, _deathSoundVolume);
        PSManager.Instance.SpawnPS(_deathPS, transform.position);

        // Deactivate the player
        gameObject.SetActive(false);
    }
}
