using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
                _enemyList.RemoveAll(item => item.IsDead());
                _unitToSpawn.transform.position = new Vector3(transform.position.x + Random.Range(-1,+1), _unitToSpawn.transform.position.y,
                                                              transform.position.z + Random.Range(-1, +1));

                _enemyList.Add(Instantiate(_unitToSpawn));
            }
            yield return new WaitForSeconds(_timeToSpawn);
        }       
    }
}
