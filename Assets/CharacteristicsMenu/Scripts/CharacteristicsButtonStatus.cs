using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CharacteristicsButtonStatus : AbstractButtonStatus
{

    [SerializeField] private string _value;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _lvlText;

    public delegate void ButtonHandler(string data, CharacteristicsButtonStatus obj);
    public event ButtonHandler buttonSelected;

    protected override void ButtonIsSelected()
    {
        buttonSelected?.Invoke(_value, this);
    }

    public void SetButtonText(string text) { _costText.text = text; }
    public void SetLevelText(string text) { _lvlText.text = text; }
}
