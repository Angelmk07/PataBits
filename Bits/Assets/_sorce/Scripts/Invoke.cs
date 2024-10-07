using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Invoke : MonoBehaviour
{
    [SerializeField]
    private enum Arrow { Up, Down, Left, Right }

    [SerializeField]
    private GameObject Show;

    [SerializeField]
    private Image[] PoinsImage;

    [SerializeField]
    private Sprite[] ArrowSprite;

    [SerializeField]
    private Arrow[] currentCombo;

    [SerializeField]
    private Arrow[] inputCombo;

    [SerializeField]
    private int currentIndex = 0;

    private GameObject[] InstansArrow = new GameObject[4];

    [SerializeField]
    private float maxTime = 5f;
    private float currentTime;

    public Action InvisibleLost; 

    private void Start()
    {
        currentTime = maxTime;
        GenerateCombo();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            InvisibleLost?.Invoke(); 
            ResetCombo(); 
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            InputArrow(Arrow.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            InputArrow(Arrow.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputArrow(Arrow.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            InputArrow(Arrow.Right);
        }
    }
    public void GenerateCombo()
    {
        Show.SetActive(true);
        currentCombo = new Arrow[4];
        for (int i = 0; i < 4; i++)
        {
            int ran = UnityEngine.Random.Range(0, 4);
            currentCombo[i] = (Arrow)ran;
            PoinsImage[i].sprite = ArrowSprite[ran];
        }
        inputCombo = new Arrow[4];
        currentIndex = 0;
        currentTime = maxTime; 
    }
    private void InputArrow(Arrow arrow)
    {
        inputCombo[currentIndex] = arrow;
        currentIndex++;
        if (currentIndex == 4)
        {
            if (CompareCombos(currentCombo, inputCombo))
            {
                maxTime -= 0.5f; 
                if (maxTime < 1f) maxTime = 1f;
                ResetCombo();


            }
            else
            {
                ResetCombo(); 
            }
        }
    }
    private void ResetCombo()
    {
        currentIndex = 0;
        GenerateCombo();
    }
    private bool CompareCombos(Arrow[] combo1, Arrow[] combo2)
    {
        for (int i = 0; i < 4; i++)
        {
            PoinsImage[i].sprite = null;
            if (combo1[i] != combo2[i])
            {
                return false;
            }
        }
        return true;
    }
}