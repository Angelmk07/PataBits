using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowLives : MonoBehaviour
{
    public static Action win;
    int lvl = 0;
    [SerializeField]
    Sprite[] sprits;
    [SerializeField]
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
        if (lvl >= sprits.Length)
        {
            win?.Invoke();
            SceneManager.LoadScene("End");
            return;
        }
        sprit.sprite = sprits[lvl];
        lvl++;
    }
}
