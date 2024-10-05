using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InvokerMetods : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] Texts;
    [SerializeField]
    private KeyCode[] Combination_1;
    [SerializeField]
    private KeyCode[] Combination_2;
    [SerializeField]
    private KeyCode[] Combination_3;
    private int index;
    private List<KeyCode> CombinationUsed = new List<KeyCode>();
    private KeyCode[] currentCombination;

    void Start()
    {
        index = 0;
        ResetCombination();
        ShowKeys();
    }

    private void OnEnable()
    {
        LisenKeyInputInBit.Pressed += ActiveCombination;
        LisenKeyInputInBit.TimeOut += ResetCombination;
    }

    private void OnDisable()
    {
        LisenKeyInputInBit.Pressed -= ActiveCombination;
        LisenKeyInputInBit.TimeOut -= ResetCombination;
    }

    private void ActiveCombination(KeyCode ActiveKey)
    {
        if (IsCorrectCombination(Combination_1, ActiveKey))
        {
            TrueKey();
            if (index >= Combination_1.Length)
            {
                Combination1Action(); 
                ResetCombination();
            }
            return;
        }
        else if (IsCorrectCombination(Combination_2, ActiveKey))
        {
            TrueKey();
            if (index >= Combination_2.Length)
            {
                Combination2Action(); 
                ResetCombination();
            }
            return;
        }
        else if (IsCorrectCombination(Combination_3, ActiveKey))
        {
            TrueKey();
            if (index >= Combination_3.Length)
            {
                Combination3Action();
                ResetCombination();
            }
            return;
        }
        else
        {
            ResetCombination();
        }
    }
    private bool IsCorrectCombination(KeyCode[] combination, KeyCode ActiveKey)
    {
        if (combination[index] == ActiveKey)
        {
            index++;
            return true;
        }
        return false;
    }

    private void Combination1Action()
    {
        Debug.Log("Executing action for Combination 1");
    }

    private void Combination2Action()
    {
        Debug.Log("Executing action for Combination 2");
    }

    private void Combination3Action()
    {
        Debug.Log("Executing action for Combination 3");
    }

    private void ShowKeys()
    {
        for (int i = 0; i < Texts.Length && i < CombinationUsed.Count; i++)
        {
            Texts[i].text = $"{CombinationUsed[i]}";
        }
    }

    private void TrueKey()
    {
        if (index - 1 < Texts.Length)
        {
            Texts[index - 1].color = Color.green;
        }
    }

    private void ResetCombination()
    {
        foreach (var text in Texts)
        {
            text.color = Color.white;
        }
        index = 0;
    }
}