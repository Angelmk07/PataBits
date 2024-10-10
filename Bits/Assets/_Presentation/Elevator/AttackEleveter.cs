using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackEleveter : MonoBehaviour
{

    [SerializeField]
    private Animator[] Animator;

    [SerializeField]
    private GameObject[] PointForAlert;

    [SerializeField]
    private AnimationClip[] _animationClip;

    [SerializeField]
    private GameObject Alert;

    [SerializeField]
    private TextMeshProUGUI AlertTime;

    [SerializeField]
    private float TimeAttack;

    [SerializeField]
    private float TimeNextAttack;

    [SerializeField]
    private int Index = 0;

    [SerializeField]
    private float TimePreHitFix = 8;

    private float TimePreHit = 0;

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
                TimeNextAttack = _animationClip[Index].length;
                Alert.transform.position = PointForAlert[Index].transform.position;
                TimePreHit = 8;
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
        AlertTime.text = $"{Mathf.RoundToInt(TimePreHit-=Time.deltaTime)}";
    }

}
