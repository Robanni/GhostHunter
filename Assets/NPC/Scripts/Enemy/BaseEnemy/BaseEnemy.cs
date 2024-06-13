using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private int _money = 5;
    private bool _isDead = false;

    protected float _currentHealth;

    public event Action<float> onHealthChanged;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        

        if (_currentHealth < 0) _currentHealth = 0;

        onHealthChanged?.Invoke(_currentHealth/_maxHealth);

        if (_currentHealth == 0 && IsDead() == false)
        {
            Die();
        }
    }



    protected void Die()
    {
        Destroy(this.gameObject);
        _isDead = true;
        PlayerCharacteristics.Instance.AddMoney(_money);
    }

    public bool IsDead()
    {
        return _isDead;
    }
}
