using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InvokerMetods : MonoBehaviour
{
    public delegate void PlayerOperation();
    [SerializeField]
    private TextMeshProUGUI[] Texts;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Vector3 PlayerSpeed = new Vector3(0.003f, 0, 0);

    [SerializeField]
    private KeyCode[] Combination_1;
    [SerializeField]
    private KeyCode[] Combination_2;
    [SerializeField]
    private KeyCode[] Combination_3;
    private Rigidbody2D PlayerRb;
    private int index;
    private List<KeyCode> CombinationUsed = new List<KeyCode>();
    private float lastKeyTime;
    private float timeLimit = 2.0f;
    private Coroutine coroutine;
    void Start()
    {
        index = 0;
        ResetCombination();
        PlayerRb = Player.GetComponent<Rigidbody2D>();
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
                coroutine = StartCoroutine( Move(Combination1Action, 2));
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
                coroutine = StartCoroutine(Move(Combination2Action, 2));
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
        Player.transform.position += new Vector3(0.003f, 0, 0);
    }

    private void Combination2Action()
    {
        Player.transform.position -= new Vector3(0.003f, 0, 0);
    }
    
    private void Combination3Action()
    {
        PlayerRb.AddForce((Player.transform.up * 7 + Player.transform.right * 3), ForceMode2D.Impulse);
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
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        CombinationUsed.Add(key);
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
            text.color = Color.black;
        }
        index = 0;
        CombinationUsed.Clear();
        lastKeyTime = Time.time;
    }
    IEnumerator Move(PlayerOperation operation, float duration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            operation();

            timeElapsed += Time.deltaTime;
            yield return null; 
        }
    }
    private IEnumerator Correct()
    {
        foreach (var text in Texts)
        {
            text.color = Color.green;
        }
        float blinkDuration = 0.2f;
        int blinkCount = 4;
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