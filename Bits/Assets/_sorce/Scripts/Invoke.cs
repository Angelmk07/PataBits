using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoke : MonoBehaviour
{
    [SerializeField]
    private enum Arrow { ¬верх, ¬низ, ¬лево, ¬право }
    [SerializeField]
    private GameObject Show;
    [SerializeField]
    private GameObject[] Poins;
    [SerializeField]
    private GameObject[] ArrowPrefub;
    [SerializeField]
    private Arrow[] currentCombo;
    [SerializeField]
    private Arrow[] inputCombo;
    [SerializeField]
    private int currentIndex = 0;
    private GameObject[] InstansArrow = new GameObject[4];


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            InputArrow(Arrow.¬верх);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            InputArrow(Arrow.¬низ);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputArrow(Arrow.¬лево);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            InputArrow(Arrow.¬право);
        }
    }

    public void GenerateCombo()
    {
        Show.SetActive(true);
        currentCombo = new Arrow[4];
        for (int i = 0; i < 4; i++)
        {
            int ran= Random.Range(0, 4);
            currentCombo[i] = (Arrow)ran;
            InstansArrow[i]= Instantiate(ArrowPrefub[ran], Poins[i].transform);
        }
        inputCombo = new Arrow[4];
    }

    private void InputArrow(Arrow arrow)
    {
        inputCombo[currentIndex] = arrow;
        currentIndex++;
        if (currentIndex == 4)
        {
            if (CompareCombos(currentCombo, inputCombo))
            {
                Debug.Log("Cool");
            }
            else
            {
                Debug.Log("Bad");
            }
            Show.SetActive(false);

            currentIndex = 0;
        }
    }
    private bool CompareCombos(Arrow[] combo1, Arrow[] combo2)
    {
        for (int i = 0; i < 4; i++)
        {
           Destroy( InstansArrow[i]);
            if (combo1[i] != combo2[i])
            {
                return false;
            }
        }
        return true;
    }
}
