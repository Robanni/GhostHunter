using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] float _baseRegeneration = 1;
    [SerializeField] private float _RegenerationSpeed = 3;
    
    private float _percentageRegeneration = 0;

    private PlayerLife _player = null;

    private void Start()
    {
        StartCoroutine(HealPlayer());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var p  =  other.GetComponent<PlayerLife>();

            if (!p.Equals(null))
            {
                _player = p;

                _percentageRegeneration = PlayerCharacteristics.Instance.GetMaxHealth()/10;


            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = null;
        }
    }

    private IEnumerator HealPlayer()
    {
        while (true)
        {

            if (_player!=null)
            {
                if(_player.IsDead() == true){_player = null; }
                if(_player != null)_player.RegenerateHealth(_baseRegeneration + _percentageRegeneration);
            }

            yield return new WaitForSecondsRealtime(_RegenerationSpeed);
        }
    }
}
