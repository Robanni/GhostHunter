using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spawner : MonoBehaviour
{
    [SerializeField] private BaseEnemy _unitToSpawn;
    [SerializeField] private float _timeToSpawn = 10f;
    [SerializeField] private int _maxUnitsOnSpot = 3;
    private Transform _playerTransform;
    
    
    private List<BaseEnemy> _enemyList ;

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();

        _enemyList = new List<BaseEnemy>();

        StartCoroutine(SpawnUnits());
    }

    private IEnumerator SpawnUnits()
    {
        while(true)
        {

            _enemyList.RemoveAll(item => item.IsUnityNull());
            if (_enemyList.Count < _maxUnitsOnSpot) 
            {
                
                
                //_unitToSpawn.transform.position = new Vector3(transform.position.x + Random.Range(-1,+1), _unitToSpawn.transform.position.y,
                                                              //transform.position.z + Random.Range(-1, +1));

                Vector3 spawnPosition = GetValidSpawnPosition();
                if (!spawnPosition.Equals(Vector3.zero)) _enemyList.Add(Instantiate(_unitToSpawn, spawnPosition, Quaternion.identity));
            } 

            yield return new WaitForSeconds(_timeToSpawn);
        }       
    }
    private Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-1, +1), _unitToSpawn.transform.position.y, transform.position.z + Random.Range(-1, +1));

        if (Vector3.Distance(_playerTransform.position,spawnPosition)<=3) // Проверка на пересечение
        {
            return Vector3.zero;
        }

        return spawnPosition;
    }
}
