using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private int _playerLvl = 0;

    [SerializeField] private Gate _gate1;
    [SerializeField] private Gate _gate2;
    [SerializeField] private Gate _gate3;
    [SerializeField] private Gate _gate4;
    [SerializeField] private Gate _gate5;
    private void Start()
    {
        PlayerCharacteristics.Instance.onPlayerLvlChanged += UpdatePlayerLvl;
    }

    private void UpdatePlayerLvl(int lvl)
    {
        _playerLvl = lvl;

        OpenGate();
    }

    //¯\_(ツ)_/¯;     (ｏ・_・)ノ”(ノ_<、)
    private void OpenGate()
    {
        switch (_playerLvl)
        {
            case 1:
                _gate1.Open();
                break;

            case 2:
                _gate2.Open();
                break;

            case 3:
                _gate3.Open();
                break;

            case 4:
                _gate4.Open();
                break;

            case 5:
                _gate5.Open();
                break;

        }
    }
}

