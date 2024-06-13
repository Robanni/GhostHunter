using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsButtonObserver : MonoBehaviour
{
    [SerializeField] private List<CharacteristicsButtonStatus> _buttonStatuses;
    [SerializeField] private CharacteristicsMenuHandler _handler;
    private void Awake()
    {
        for (int i = 0; i < _buttonStatuses.Count; i++)
        {
            _buttonStatuses[i].buttonSelected += SendStatus;
        }
    }
    private void SendStatus(string data, CharacteristicsButtonStatus obj)
    {
        _handler.GetCharacteristicName(data,obj);
    }
}
