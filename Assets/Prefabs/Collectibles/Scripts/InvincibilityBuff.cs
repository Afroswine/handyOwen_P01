using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityBuff : PowerUpBase
{
    [Header("Invincibility")]
    [SerializeField] Material _invincibleMaterial;

    protected override void PowerUp()
    {
        if (_player != null)
        {
            _player.IsInvincible = true;
            _player.Recolor(_invincibleMaterial);
        }
    }

    protected override void PowerDown()
    {
        _player.Recolor(_player.BodyMaterial);
        _player.IsInvincible = false;
    }
}
