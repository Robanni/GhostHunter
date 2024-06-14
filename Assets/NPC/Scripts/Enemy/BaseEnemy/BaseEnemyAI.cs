using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseEnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _detectionRadius = 10f;
    [SerializeField] private Transform _enemyTransform;
    private Transform _target;
    private bool _isPlayerInRange = false;

    private Vector3 _moveDirection = new Vector3(0,0,0);

    [SerializeField]private float _timeOfChanging = 3;
    private float _timerOfChangingDirection = 0;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = _detectionRadius;
    }

    void FixedUpdate()
    {
        if (_isPlayerInRange)
        {

            if (_target.Equals(null)) return;
            MoveTowardsPlayer();

            return;
        }

        RandomMovement();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (_target.position - _enemyTransform.position).normalized;

        _enemyTransform.position += direction * _speed * Time.deltaTime;

        _enemyTransform.LookAt(new Vector3(_target.position.x,transform.position.y,_target.position.z));
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
