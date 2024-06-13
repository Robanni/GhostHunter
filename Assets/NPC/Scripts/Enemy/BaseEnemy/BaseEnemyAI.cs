using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private Transform enemyTransform;
    private Transform target;
    private bool isPlayerInRange = false; 

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = detectionRadius;
    }

    void FixedUpdate()
    {
        if (isPlayerInRange)
        {

            if (target.Equals(null)) return;
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (target.position - enemyTransform.position).normalized;

        enemyTransform.position += direction * speed * Time.deltaTime;

        enemyTransform.LookAt(new Vector3(target.position.x,transform.position.y,target.position.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
