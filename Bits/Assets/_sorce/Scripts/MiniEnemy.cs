using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemy : MonoBehaviour
{
    public static Action MiniDead;

    [SerializeField]
    private Vector3[] _patrolPoint;
    [SerializeField]
    private int live = 1;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float detectionRadius = 5f;  
    [SerializeField]
    private LayerMask playerLayer;      
    private int _index = 0;
    private float _threshold = 0.1f;
    [SerializeField]
    private Transform playerTransform;  
    private bool isChasingPlayer = false;

    private void Update()
    {
        DetectPlayer();
        if (playerTransform != null)
        {
            if (Vector3.Distance(playerTransform.position, transform.position) <= 1)
            {
                return;
            }
        }
        if (isChasingPlayer && playerTransform != null)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Vector2 direction = (_patrolPoint[_index] - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, _patrolPoint[_index], _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _patrolPoint[_index]) < _threshold)
        {
            _index++;
            if (_index >= _patrolPoint.Length)
            {
                _index = 0;
            }
        }
    }

    private void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (hit != null)
        {
            playerTransform = hit.transform;
            isChasingPlayer = true; 
        }
        else
        {
            isChasingPlayer = false; 
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2( playerTransform.position.x,transform.position.y), _speed * Time.deltaTime);

    }

    public void TakeHit(int value)
    {
        live -= value;
        if (live < 1)
        {
            MiniDead?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}