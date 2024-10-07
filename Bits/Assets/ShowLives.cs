using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLives : MonoBehaviour
{
    public static Action win;
    int lvl = 0;
    [SerializeField]
    Sprite[] sprits;
    SpriteRenderer sprit;
    private void OnEnable()
    {
        Lives.hit += addlvl;
    }
    private void OnDisable()
    {
        Lives.hit -= addlvl;
    }
    private void addlvl()
    {
        sprit.sprite = sprits[lvl];
        lvl++;
        if (lvl > 90)
        {
            win?.Invoke();
        }
    }
}
