using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerS : MonoBehaviour
{
    public static Action Dead;
    [field: SerializeField]
    public Vector3 playerSpeed { private set; get; } = new Vector3(0.03f, 0, 0);
    [field: SerializeField]
    public Rigidbody2D PlayerRb { private set; get; }
    [field: SerializeField]
    public GameObject Player { private set; get; }
    [field: SerializeField]
    public SpriteRenderer _spriteR { private set; get; }

    [field: SerializeField]
    public int high { private set; get; } = 7;

    [field: SerializeField]
    public int forvard { private set; get; } = 3;

    [field: SerializeField]
    public float MoveTime { private set; get; } = 25;

    [field: SerializeField]
    public GameObject PlayerAtackPoint { private set; get; }

    [field: SerializeField]
    public GameObject PlayerAtackPointRotation { private set; get; }
    [field: SerializeField]
    public int AttackRadius { private set; get; }
    [field: SerializeField]
    public int Health { private set; get; }
    [field: SerializeField]
    public bool isHide { private set; get; }
    [field: SerializeField]
    public bool dead { private set; get; }
    [field: SerializeField]
    public int power { private set; get; }
    [SerializeField]
    private int TimeHide=2;
    [SerializeField]
    private SpriteRenderer _spriteRender;
    [field: SerializeField]
    public Animator PlayerAnimator { private set; get; }
    private bool Finded;

    private void Start()
    {
        isHide = false;
    }
    private void OnEnable()
    {
        FieldOfView.Spoted += FindedMetod;
        LandingCheck.Landing += LandAnim;
        Panic.InvisibleLost += HideMech;
    }

    private void LandAnim()
    {
        if (PlayerAnimator != null)
        {
            PlayerAnimator.SetTrigger("Landing");
        }

    }

    private void OnDisable()
    {
        FieldOfView.Spoted -= FindedMetod;
        Panic.InvisibleLost -= HideMech;

    }
    private void OnBecameInvisible()
    {
        Dead?.Invoke();
    }

    protected void FindedMetod()
    {
        Finded = true;
        dead = true;
        Dead?.Invoke();
    }
    public void HideMech()
    {
        isHide = !isHide;
        if (isHide)
        {
            StartCoroutine(FadeTo(0, TimeHide)); 
        }
        else
        {
            StartCoroutine(FadeTo(1, TimeHide)); 
        }
    }
    public void TakeHit(int value)
    {
        Health -= value;
        if (Health < 1)
        {
            dead = true;
            Dead?.Invoke();
        }
    }
    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = _spriteRender.color.a;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            _spriteRender.color = new Color(_spriteRender.color.r, _spriteRender.color.g, _spriteRender.color.b, newAlpha);
            yield return null;
        }
        _spriteRender.color = new Color(_spriteRender.color.r, _spriteRender.color.g, _spriteRender.color.b, targetAlpha);
    }

}
