using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    Vector3 lastpos;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Vector3[] _patrolPoint;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _threshold = 0.1f;
    private int _index = 0;

    private void Update()
    {
        Patrol();
    }
    private void Patrol()
    {
        lastpos = transform.position;
        Vector2 direction = (_patrolPoint[_index] - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position,new Vector2( _patrolPoint[_index].x,transform.position.y), _speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, new Vector2(_patrolPoint[_index].x, transform.position.y)) < _threshold)
        {
            _index++;
            _index %= _patrolPoint.Length;
        }
        
        if (transform.position.x > lastpos.x)
        {
            animator.SetBool("Forward", true);
            animator.SetBool("Back", false);
        }
        else if (transform.position.x < lastpos.x)
        {
            animator.SetBool("Back", true);
            animator.SetBool("Forward", false);
        }

    }
}
