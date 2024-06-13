using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusBarHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _money;
    [SerializeField] TextMeshProUGUI _health;

    [SerializeField] PlayerLife _playerLife;

    private void Start()
    {
        _health.text = PlayerCharacteristics.Instance.GetMaxHealth().ToString();
        _money.text = PlayerCharacteristics.Instance.GetMoney().ToString();

        _playerLife.onCurrentHealthChanged += SetPlayerHealth;
        PlayerCharacteristics.Instance.onMoneyChanged += SetPlayerMoney;
    }

    private void SetPlayerHealth(float health)
    {
        _health.text = health.ToString();
    }

    private void SetPlayerMoney()
    {
        _money.text = PlayerCharacteristics.Instance.GetMoney().ToString();
    }
}
