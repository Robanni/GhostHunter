using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;

    private List<IDamageable> _enemies;

    private void Start()
    {
        _enemies = new List<IDamageable>();
        StartCoroutine(DamageEnemy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.tag);

            IDamageable enemy = other.GetComponent<IDamageable>();

            if (!enemy.Equals(null))
            {
                _enemies.Add(enemy);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            IDamageable enemy = other.GetComponent<IDamageable>();

            if (!enemy.Equals(null))
            {
                _enemies.Remove(enemy);
            }
        }
    }

    private IEnumerator DamageEnemy()
    {
        while (true)
        {
            if (!_enemies.Equals(null))
            {
                DelitingDeadEnemies();
                foreach (var e in _enemies)
                {
                    e.TakeDamage(_damage);
                }
            }


            yield return new WaitForSecondsRealtime(1);
        }
    }

    private void DelitingDeadEnemies()
    {
        List<IDamageable> newList = new List<IDamageable>();
        foreach (var e in _enemies)
        {
            if (!e.IsDead()) newList.Add(e);
        }
        _enemies = newList;
    }
}
