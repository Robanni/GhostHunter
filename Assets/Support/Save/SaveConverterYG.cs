using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SaveConverterYG : MonoBehaviour
{
    private  Dictionary<string, int> _characteristicsLvl;
    private bool GetPlayerCharacteristics()
    {
        return false;
    }

    private bool SetPlayerCharacteristics(Dictionary<string, int> charact)
    {
        _characteristicsLvl = charact;

        return false;
    }

    [DllImport("__Internal")]
    private static extern void ShowDeathAdv();
}
