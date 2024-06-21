using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacteristicsMenuHandler : MonoBehaviour
{
    private int _basePrice = 10;
    private bool _active = false;
    public void GetCharacteristicName(string characteristicName, CharacteristicsButtonStatus obj)
    {
        Debug.Log(characteristicName);
        UpgradeCharacteristic(characteristicName,obj);

        switch (characteristicName)
        {
            case "Speed":
                Debug.Log(PlayerCharacteristics.Instance.GetCharacteristicLvl(characteristicName));

                break;
            case "Damage":
                Debug.Log(PlayerCharacteristics.Instance.GetCharacteristicLvl(characteristicName));
                break;
            case "Health":
                Debug.Log(PlayerCharacteristics.Instance.GetCharacteristicLvl(characteristicName));
                break;
        }
    }

    private void UpgradeCharacteristic(string name, CharacteristicsButtonStatus obj)
    {

        int lvl = PlayerCharacteristics.Instance.GetCharacteristicLvl(name);
        int money = PlayerCharacteristics.Instance.GetMoney();

        if (money >= (_basePrice + _basePrice * lvl))
        {
            PlayerCharacteristics.Instance.RemoveMoney(_basePrice + _basePrice * lvl);
            obj.SetButtonText( (_basePrice + _basePrice * lvl).ToString() );
            PlayerCharacteristics.Instance.SetCharacteristicLvl(name, lvl+1);
            obj.SetLevelText((lvl + 1).ToString() + "/150");
        }
    }
        
    public void ChangeMenuVisability()
    {
        _active = !_active;
        gameObject.SetActive(_active);
        if(_active) { Time.timeScale = 0; } else { Time.timeScale = 1; }
    }
}
