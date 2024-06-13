using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private float _attackRadius = 1f;
    private float _damage = 1f;

    private LineRenderer _lineRenderer;

    private List<IDamageable> _enemies;
    private List<Transform> _targets;




    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _targets = new List<Transform>();

        _enemies = new List<IDamageable>();

        _damage = PlayerCharacteristics.Instance.GetDamage();
        _attackRadius = PlayerCharacteristics.Instance.GetAttackRadius();
        PlayerCharacteristics.Instance.onCharacteristicChanged += UpdataCharacteristic;

        ChangeAttackRadius();

        StartCoroutine(DamageEnemy());
    }

    private void LateUpdate()
    {
        DrawLinesToEnemies();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            IDamageable enemy = other.GetComponent<IDamageable>();

            if (!enemy.Equals(null))
            {
                _enemies.Add(enemy);
                _targets.Add(other.GetComponent<Transform>());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            IDamageable enemy = other.GetComponent<IDamageable>();
            if (!enemy.Equals(null))
            {
                _enemies.Remove(enemy);
                _targets.Remove(other.GetComponent<Transform>());
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

    private void UpdataCharacteristic()
    {
        _damage = PlayerCharacteristics.Instance.GetDamage();
        _attackRadius = PlayerCharacteristics.Instance.GetAttackRadius();

        ChangeAttackRadius();
    }

    private void ChangeAttackRadius()
    {
        transform.localScale = new Vector3(_attackRadius, _attackRadius, _attackRadius);
    }

    private void DrawLinesToEnemies()
    {
        if (_targets.Count <= 0) {_lineRenderer.enabled = false; return; }

        _targets.RemoveAll(item => item == null);

        _lineRenderer.enabled = true;

        _lineRenderer.positionCount = _targets.Count * 2;

        int index = 0;

        foreach (var enemy in _targets)
        {
            _lineRenderer.SetPosition(index, transform.position);
            _lineRenderer.SetPosition(index + 1, enemy.position);
            index += 2;
        }
    }
}
