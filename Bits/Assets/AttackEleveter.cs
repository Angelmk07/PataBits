using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEleveter : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Animator[] Animator;
    [SerializeField]
    private float TimeAttack;
    [SerializeField]
    private float TimeNextAttack;
    [SerializeField]
    private int Index = 0;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (TimeAttack >= TimeNextAttack)
        {
            TimeAttack += Time.deltaTime;
            if (TimeAttack >= TimeNextAttack + 1.5f)
            {
                Index = Random.Range(0, Animator.Length);
                Animator[Index].SetBool("Attack", true);
                TimeAttack = 0;
            }
            else
            {
                Animator[Index].SetBool("Attack", false);
            }
        }
        if (TimeAttack < TimeNextAttack)
        {
            TimeAttack += Time.deltaTime;
        }
    }
}
