using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private BaseEnemy _unitToSpawn;
    [SerializeField] private float _timeToSpawn = 10f;
    [SerializeField] private int _maxUnitsOnSpot = 3;
    
    private List<BaseEnemy> _enemyList ;

    private void Start()
    {
        _enemyList = new List<BaseEnemy>();

        StartCoroutine(SpawnUnits());
    }

    private IEnumerator SpawnUnits()
    {
        while(true)
        {
            if (_enemyList.Count < _maxUnitsOnSpot) 
            {
                _unitToSpawn.transform.position = new Vector3(transform.position.x + Random.Range(-3,+3), _unitToSpawn.transform.position.y,
                                                              transform.position.z + Random.Range(-3, +3));

                _enemyList.Add(Instantiate(_unitToSpawn));
            }
            yield return new WaitForSeconds(_timeToSpawn);
        }       
    }
}
