using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Panic : MonoBehaviour
{
    [SerializeField]
    private enum Arrow { Up, Down, Left, Right }

    [SerializeField]
    private GameObject Show;

    [SerializeField]
    private Image ShowTime;

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

    [SerializeField]
    private Sprite[] Corect ;

    [SerializeField]
    private float maxTime = 5f;
    private float currentTime;
    public static Action InvisibleLost;
    private int[] rands = new int[4];

    private void Awake()
    {
        currentTime = maxTime;
        GenerateCombo();
        currentTime = maxTime;
        ShowTime.fillAmount = 1;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        ShowTime.fillAmount = currentTime/maxTime ;
        if (currentTime <= 0f)
        {
            InvisibleLost?.Invoke();
            resetTimer();
            Show.SetActive(false);
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
        if(Show.active == true&&Input.GetKeyDown(KeyCode.Space))
        {
            InvisibleLost?.Invoke();
            resetTimer();
            Show.SetActive(false);
            return;
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
            rands[i] = ran;
        }
        inputCombo = new Arrow[4];
        currentIndex = 0;

    }
    private void resetTimer()
    {
        ShowTime.fillAmount = 1;
        currentTime = maxTime;

    }
    private void InputArrow(Arrow arrow)
    {
        inputCombo[currentIndex] = arrow;
        if (!CompareArrows(inputCombo[currentIndex], currentCombo[currentIndex]))
        {
            ResetCombo();

        }
        else
        {
            PoinsImage[currentIndex].sprite = Corect[rands[currentIndex]];
            currentIndex++;
        }


        if (currentIndex == 4)
        {
            if (CompareCombos(currentCombo, inputCombo))
            {
                maxTime -= 0.5f; 
                if (maxTime < 2f) maxTime = 2f;
                ResetCombo();
                resetTimer();
            }
            else
            {
                ResetCombo(); 
            }
        }
    }
    private void ResetCombo()
    {
        GenerateCombo();
        currentIndex = 0;
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
    private bool CompareArrows(Arrow Arrow1, Arrow Arrow2)
    {
        if(Arrow1 != Arrow2)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
}