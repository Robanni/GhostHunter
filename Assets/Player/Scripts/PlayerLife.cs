using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour,IDamageable
{

    private float _maxHealth = 100f;
    private bool _isDead = false;

    protected float _currentHealth;

    private void Start()
    {
        _maxHealth = PlayerCharacteristics.Instance.GetMaxHealth();
        _currentHealth = _maxHealth;

        PlayerCharacteristics.Instance.onCharacteristicChanged += UpdataCharacteristic;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;


        if (_currentHealth < 0) _currentHealth = 0;

        onCurrentHealthChanged?.Invoke(_currentHealth);

        if (_currentHealth == 0 && !IsDead())
        {
            Die();
        }
    }



    protected void Die()
    {
        Destroy(this.gameObject);
        _isDead = true;
    }

    public bool IsDead()
    {
        return _isDead;
    }

    private void UpdataCharacteristic()
    {
        _maxHealth = PlayerCharacteristics.Instance.GetMaxHealth();
    }

    public event Action<float> onCurrentHealthChanged;
}
