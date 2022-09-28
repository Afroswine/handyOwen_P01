using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    // backing field
    [Header("Player")]
    [SerializeField] int _maxHealth = 3;
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
    [Tooltip("PS to play when the player is killed.")]
    [SerializeField] ParticleSystem _deathPS;
    [Tooltip("Audio to play when the player is killed.")]
    [SerializeField] AudioClip _deathSound;
    [Tooltip("PS to play when the player blocks an attack.")]
    [SerializeField] ParticleSystem _blockPS;
    [Tooltip("Audio to play when the player blocks an attack.")]
    [SerializeField] AudioClip _blockSound;

    public int MaxHealth
    {
        get { return _maxHealth; }
    }
    int _currentHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        private set
        {
            if (value > _maxHealth)
                value = _maxHealth;
            _currentHealth = value;
        }
    }
    int _treasureCount = 0;
    public int TreasureCount => _treasureCount;
    
    [Header("States")]
    public bool IsInvincible = false;

    [HideInInspector] public UnityEvent m_HealthUpdate = new UnityEvent();
    [HideInInspector] public UnityEvent m_PlayerDeath = new UnityEvent();
    [HideInInspector] public UnityEvent m_TreasureUpdate = new UnityEvent();
    
    TankController _tankController;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        m_HealthUpdate.Invoke();
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        m_HealthUpdate.Invoke();
    }

    public void DecreaseHealth(int amount)
    {
        // if the player isn't invincible
        if (!IsInvincible)
        {
            AudioHelper.PlayClip2D(_hurtSound, 1f);
            _hurtPS.Play();

            _currentHealth -= amount;
            m_HealthUpdate.Invoke();

            if (_currentHealth <= 0)
                Kill();
        }
        // if the player *is* invincible
        else
        {
            AudioHelper.PlayClip2D(_blockSound, 1f);
            _blockPS.Play();
        }
    }
    
    public void IncreaseTreasure(int amount)
    {
        _treasureCount += amount;
        m_TreasureUpdate.Invoke();
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
        AudioHelper.PlayClip2D(_deathSound, 1f);
        
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ParticleSystem deathPS = Instantiate(_deathPS, position, Quaternion.identity);
        deathPS.Play();

        // Guarantee health is at 0
        _currentHealth = 0;

        // Call events
        m_HealthUpdate.Invoke();
        m_PlayerDeath.Invoke();

        // Deactivate the player
        gameObject.SetActive(false);
    }
}
