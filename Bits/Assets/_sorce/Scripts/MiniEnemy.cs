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
    private int _index = 0;
    private float _threshold = 0.1f;

    private void Update()
    {
        Vector2 direction = (_patrolPoint[_index] - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, _patrolPoint[_index], _speed * Time.deltaTime);
        if (Vector2.Distance(gameObject.transform.position, _patrolPoint[_index]) < _threshold)
        {
            _index++;
            if (_index >= _patrolPoint.Length)
            {
                _index = 0;
            }
        }
    }
    public void TakeHit(int vlaue)
    {
        live-=vlaue;
        if (live < 1)
        {
            MiniDead?.Invoke();
            Destroy(gameObject);
        }
    }
}
