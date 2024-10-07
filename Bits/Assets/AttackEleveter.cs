using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEleveter : MonoBehaviour
{
    [SerializeField]
    private GameObject Danger;
    private GameObject danger;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Animator Animator;
    [SerializeField]
    private float TimeAttack;
    [SerializeField]
    private float TimeNextAttack;
    [SerializeField]
    private int ValueDanger = 0;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (TimeAttack >= TimeNextAttack)
        {
            if (ValueDanger == 0)
            {
                danger = Instantiate(Danger, Player.transform.position, Quaternion.identity);
                ValueDanger++;
            }
            if (ValueDanger == 1)
            {
                TimeAttack += Time.deltaTime;
                if (TimeAttack >= TimeNextAttack + 1.5f)
                {
                    transform.position = new Vector3(danger.transform.position.x, transform.position.y);
                    Animator.SetBool("Attack", true);
                    TimeAttack = 0;
                    ValueDanger = 0;
                    Destroy(danger);
                }
                else
                {
                    Animator.SetBool("Attack", false);
                }
            }
        }
        if (TimeAttack < TimeNextAttack)
        {
            TimeAttack += Time.deltaTime;
        }
    }
}
