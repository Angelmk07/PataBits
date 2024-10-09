using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEleveter : MonoBehaviour
{

    [SerializeField]
    private Animator[] Animator;

    [SerializeField]
    private AnimationClip[] AnimationClip;

    [SerializeField]
    private float TimeAttack;
    [SerializeField]
    private float TimeNextAttack;
    [SerializeField]
    private int Index = 0;
    private void Start()
    {
        Time.timeScale = 1;
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
                TimeNextAttack = AnimationClip[Index].length;
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
