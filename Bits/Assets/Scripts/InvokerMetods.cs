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
    private float lastKeyTime;
    private float timeLimit = 2.0f; 
    void Start()
    {
        index = 0;
        ResetCombination();

    }
    private void Update()
    {
        if (Time.time - lastKeyTime > timeLimit)
        {
            ResetCombination();
        }
    }
    private void OnEnable()
    {
        LisenKeyInputInBit.Pressed += ActiveCombination;
        LisenKeyInputInBit.TimeOut += ResetCombinationOutTime;
    }

    private void OnDisable()
    {
        LisenKeyInputInBit.Pressed -= ActiveCombination;
        LisenKeyInputInBit.TimeOut -= ResetCombinationOutTime;
    }

    private void ActiveCombination(KeyCode ActiveKey)
    {
        lastKeyTime = Time.time;
        if (IsCorrectCombination(Combination_1, ActiveKey))
        {
            TrueKey(ActiveKey);
            index++;
            if (index >= Combination_1.Length)
            {
                Combination1Action();
                StartCoroutine(Correct());
            }
            return;
        }
        else if (IsCorrectCombination(Combination_2, ActiveKey))
        {
            TrueKey(ActiveKey);
            index++;
            if (index >= Combination_2.Length)
            {
                Combination2Action();
                StartCoroutine(Correct());
            }
            return;
        }
        else if (IsCorrectCombination(Combination_3, ActiveKey))
        {
            TrueKey(ActiveKey);
            index++;
            if (index >= Combination_3.Length)
            {
                Combination3Action();
                StartCoroutine(Correct());
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
        for (int i = 0; i < CombinationUsed.Count; i++)
        {
            if (CombinationUsed[i] != combination[i])
            {
                return false;  
            }
        }
        return combination[index] == ActiveKey;
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

        Texts[index].text = $"{CombinationUsed[index]}";
        
    }
    private void HideKeys()
    {
        for (int i = 0; i < Texts.Length; i++)
        {
            Texts[i].text = null;
        }
    }
    private void TrueKey(KeyCode key)
    {
        CombinationUsed.Add(key);
        if (index  < Texts.Length)
        {
            Texts[index].color = Color.green;
        }
        ShowKeys();
    }
    private void ResetCombinationOutTime()
    {
        if(CombinationUsed != null)
        {
            ResetCombination();
        }

    }
    private void ResetCombination()
    {
        HideKeys();
        foreach (var text in Texts)
        {
            text.color = Color.white;
        }
        index = 0;
        CombinationUsed.Clear();
        lastKeyTime = Time.time;
    }
    private IEnumerator Correct()
    {

        float blinkDuration = 0.2f;
        int blinkCount = 7;
        for (int i = 0; i < blinkCount; i++)
        {
            SetSpriteTransparency(0.5f);
            yield return new WaitForSeconds(blinkDuration);
            SetSpriteTransparency(1f);
            yield return new WaitForSeconds(blinkDuration);
        }
    }
    private void SetSpriteTransparency(float alpha)
    {
        for(int i = 0; i < Texts.Length; i++)
        {
            Color newColor = Texts[i].color;
            newColor.a = alpha;
            Texts[i].color = newColor;
        }
    }
}