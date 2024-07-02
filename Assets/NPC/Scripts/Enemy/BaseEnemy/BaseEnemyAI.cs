using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseEnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform _enemyTransform;
    [SerializeField, Range(-1, 1)] private int _friendlyType = 1;//if 1 then enemy will try to attack, else run away
    private Transform _target;
    private bool _isPlayerInRange = false;

    private Vector3 _moveDirection = new Vector3(0,0,0);

    [SerializeField] private float _timeOfChanging = 3;
    private float _timerOfChangingDirection = 0;

    private void OnValidate()
    {
        if (_friendlyType == 0)
        {
            _friendlyType = -1; 
        }
    }
    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        SphereCollider collider = gameObject.GetComponent<SphereCollider>();

    }

    private void FixedUpdate()
    {
        if (_isPlayerInRange)
        {

            if (_target.Equals(null)) return;
            EnemyMovementType();

            return;
        }

        RandomMovement();
    }

    protected virtual void EnemyMovementType()
    {
        Vector3 direction = (_target.position - _enemyTransform.position).normalized;
        direction.y = 0;

        _enemyTransform.position += direction * _speed * Time.deltaTime * _friendlyType;

        Vector3 directionToTarget = _target.position - _enemyTransform.position;
        Vector3 lookAtPosition;

        if (_friendlyType == -1)
        {
            Vector3 directionAwayFromTarget = -directionToTarget;
            lookAtPosition = _enemyTransform.position + new Vector3(directionAwayFromTarget.x, 0, directionAwayFromTarget.z);
        }else lookAtPosition = _enemyTransform.position + new Vector3(directionToTarget.x, 0, directionToTarget.z);

        _enemyTransform.LookAt(lookAtPosition);
    }
    private void RandomMovement()
    {
        _timerOfChangingDirection += Time.deltaTime;

        if (_timerOfChangingDirection > _timeOfChanging)
        {
            float randomAngle = Random.Range(0f, 360f);
            _moveDirection = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));

            _timerOfChangingDirection = 0;
        }

        _enemyTransform.position += _moveDirection * _speed * Time.deltaTime;
        _enemyTransform.LookAt(_moveDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
        }
    }
}
