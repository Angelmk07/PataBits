using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitShow : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    private void OnDisable()
    {
        Lives.hit += hitanim;
    }
    private void OnEnable()
    {
        Lives.hit -= hitanim;
    }
    void hitanim()
    {
        animator.SetTrigger("Hit");
    }
}
