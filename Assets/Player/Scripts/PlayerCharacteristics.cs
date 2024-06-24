using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacteristics : MonoBehaviour
{
    public static PlayerCharacteristics Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] private float _maxHealth = 10f;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _attackRadius = 5f;
    [SerializeField] private int _money = 0;
    [SerializeField] private int _playerLvl = 0;
    [SerializeField] private int _playerCurrentExp = 0;


    private Dictionary<string, int> _characteristicsLvl = new Dictionary<string, int>()
    {
        { "Speed",1},
        {"Damage",1 },
        {"Health",1 },
        {"AttackRadius",1 },
        {"PlayerLvl", 0 }
    };


    public event Action onCharacteristicChanged;
    public event Action onMoneyChanged;
    public event Action<int> onPlayerLvlChanged;

    public float GetMaxHealth() { return _maxHealth; }
    public float GetDamage() { return _damage; }
    public float GetSpeed() { return _speed; }
    public float GetAttackRadius() {  return _attackRadius; }
    public int GetMoney() { return _money; }
    public int GetLevel() { return _playerLvl; }
    public int GetPlayerCurrentExp() { return _playerCurrentExp; }
    public void AddPlayerExp(int amount) { if (amount >= 0) { _playerCurrentExp += amount; TryToLevelUp(); } }
    public void AddMoney(int m) { if (_money <= 0) return; _money += m; onMoneyChanged?.Invoke(); }
    public void RemoveMoney(int money)
    {
        if ((_money - money) >= 0) _money -= money;
        else throw new Exception("Trying to remove to much money");
        onMoneyChanged?.Invoke();
    }
    public int GetCharacteristicLvl(string characteristic)
    {
        int res = 0;
        _characteristicsLvl.TryGetValue(characteristic, out res);
        return res;
    }
    public void SetCharacteristicLvl(string characteristic, int lvl)
    {
        _characteristicsLvl[characteristic] = lvl;

        switch (characteristic)
        {
            case "Speed":
                _speed += lvl * .2f;
                break;

            case "Damage":
                _damage += lvl * .5f;
                break;

            case "Health":
                _maxHealth += lvl;
                break;

            case "AttackRadius":
                _attackRadius += lvl*0.01f;
                break;
        }

        onCharacteristicChanged?.Invoke();
    }
    private void TryToLevelUp()
    {
        if(_playerCurrentExp >= (3 + _playerLvl*10))
        {
            _playerLvl++;
            _playerCurrentExp -= (3 + _playerLvl * 10);
            _characteristicsLvl["PlayerLvl"] = _playerLvl;

            onPlayerLvlChanged?.Invoke(_playerLvl);
        }
    }
}
