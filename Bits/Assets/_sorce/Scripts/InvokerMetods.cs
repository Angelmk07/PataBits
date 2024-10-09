using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InvokerMethods : MonoBehaviour
{
    public delegate void PlayerAction();

    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private PlayerS player;
    [SerializeField] private Panic hide;
    [SerializeField]
    private KeyCode[] Combination_1;
    [SerializeField]
    private KeyCode[] Combination_2;
    [SerializeField]
    private KeyCode[] Combination_3;
    [SerializeField]
    private KeyCode[] Combination_4;
    [SerializeField]
    private KeyCode[] Combination_5;
    private KeyCode[][] combinations;  
    [SerializeField] private LayerMask enemyLayer;

    private int currentIndex = 0;
    [SerializeField]
    private int MaxLenthCombination = 4;
    [SerializeField]
    private List<KeyCode> enteredKeys = new List<KeyCode>();
    private float lastKeyTime;
    private const float timeLimit = 2.0f;
    private Coroutine moveCoroutine;
    private int comboMultiplier = 1;
    private KeyCode[] lastCombo;

    private void Start()
    {
        combinations = new KeyCode[][] { Combination_1, Combination_2, Combination_3, Combination_4, Combination_5 };
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
        LisenKeyInputInBit.Pressed += HandleKeyPress;
        LisenKeyInputInBit.TimeOut += ResetCombination;
    }

    private void OnDisable()
    {
        LisenKeyInputInBit.Pressed -= HandleKeyPress;
        LisenKeyInputInBit.TimeOut -= ResetCombination;
    }

    private void HandleKeyPress(KeyCode keyPressed)
    {
        lastKeyTime = Time.time;
        if (enteredKeys.Count+1 > MaxLenthCombination)
        {
            enteredKeys.Clear();
            currentIndex = 0;
            return;
        }
        for (int i = 0; i < combinations.Length; i++)
        {
            if (IsCorrectKeySequence(combinations[i], keyPressed))
            {
                if (currentIndex >= combinations[i].Length)
                {
                    HandleComboCompletion(combinations[i], i);
                }
                return;
            }
        }

        if (keyPressed == KeyCode.Space && moveCoroutine != null)
        {

            StopWalksCorutine();
        }
        else
        {
            ResetCombination();
        }
    }

    private bool IsCorrectKeySequence(KeyCode[] combination, KeyCode keyPressed)
    {
        for (int i = 0; i < enteredKeys.Count; i++)
        {
            if (enteredKeys[i] != combination[i]) return false;
        }

        if (combination[currentIndex] == keyPressed)
        {
            enteredKeys.Add(keyPressed);
            UpdateKeyDisplay();
            currentIndex++;

            if (currentIndex >= combination.Length)
            {
                StartCoroutine(ShowCorrectSequenceFeedback());

            }

            return true;
        }

        return false;
    }

    private void HandleComboCompletion(KeyCode[] combination, int comboIndex)
    {
        UpdateComboMultiplier(combination);
        if(moveCoroutine!= null)
        {
            StopWalksCorutine();
        }
        switch (comboIndex)
        {
            case 0:
                moveCoroutine = StartCoroutine(MovePlayer(Combination1Action));
                break;
            case 1:
                moveCoroutine = StartCoroutine(MovePlayer(Combination2Action));
                break;
            case 2:
                Combination3Action();
                break;
            case 3:
                hide.GenerateCombo();
                player.HideMech();
                break;
            case 4:
                PerformAttack();
                break;
        }

    }

    private void UpdateComboMultiplier(KeyCode[] combination)
    {
        if (lastCombo == combination)
        {
            comboMultiplier++;
        }
        else
        {
            comboMultiplier = 1;
        }

        lastCombo = combination;
    }

    private void PerformAttack()
    {
        player.PlayerAnimator.SetTrigger("Atack");
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.PlayerAtackPoint.transform.position, player.AttackRadius);
        foreach (var hit in hits)
        {
            if (Utils.LayerMaskUtil.ContainsLayer(enemyLayer, hit.gameObject))
            {
                if (hit.TryGetComponent(out Lives _Boss))
                {
                    _Boss.TakeHit(player.power);

                }
            }
        }
    }

    private void Combination1Action()
    {
        player.transform.position += player.playerSpeed * comboMultiplier * Time.deltaTime;
        player.PlayerAnimator.SetBool("Walk",true);
    }

    private void Combination2Action()
    {
        player.transform.position -= player.playerSpeed * comboMultiplier*Time.deltaTime;
        player.PlayerAnimator.SetBool("WalkBehind", true);

    }

    private void Combination3Action()
    {
        player.PlayerRb.AddForce(player.transform.up * player.high * comboMultiplier + player.transform.right * player.forvard *player.PlayerAtackPointRotation.transform.localScale.x* comboMultiplier, ForceMode2D.Impulse);
        player.PlayerAnimator.SetTrigger("Jump");
    }

    private void UpdateKeyDisplay()
    {
        texts[currentIndex].text = enteredKeys[currentIndex].ToString();

    }

    private void ResetCombination()
    {
        lastCombo = null;
        ClearKeyDisplay();
        currentIndex = 0;
        enteredKeys.Clear();
        lastKeyTime = Time.time;
    }

    private void ClearKeyDisplay()
    {
        foreach (var text in texts)
        {
            text.text = string.Empty;
            text.color = Color.white;
        }
    }

    private IEnumerator MovePlayer(PlayerAction action)
    {
        float duration =(float) player.MoveTime;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            action.Invoke();
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StopWalksCorutine();
    }
    private void StopWalksCorutine()
    {
        StopCoroutine(moveCoroutine);
        player.PlayerAnimator.SetBool("Walk", false);
        player.PlayerAnimator.SetBool("WalkBehind", false);
    }
    private IEnumerator ShowCorrectSequenceFeedback()
    {

        foreach (var text in texts)
        {
            text.color = Color.green;
        }

        float blinkDuration = 0.2f;
        int blinkCount = 4;

        for (int i = 0; i < blinkCount; i++)
        {
            SetTextTransparency(0.5f);
            yield return new WaitForSeconds(blinkDuration);
            SetTextTransparency(1f);
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    private void SetTextTransparency(float alpha)
    {
        foreach (var text in texts)
        {
            var color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }
}