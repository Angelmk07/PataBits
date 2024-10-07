using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerS : MonoBehaviour
{
    [field: SerializeField]
    public Vector3 playerSpeed { private set; get; } = new Vector3(0.03f, 0, 0);
    [field: SerializeField]
    public Rigidbody2D PlayerRb { private set; get; }
    [field: SerializeField]
    public GameObject Player { private set; get; }
    [field: SerializeField]
    public GameObject PlayerAtackPoint { private set; get; }
    [field: SerializeField]
    public int AttackRadius { private set; get; }
    [field: SerializeField]
    public int Medicine { private set; get; }
    [field: SerializeField]
    public bool isHide { private set; get; }
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
        MiniEnemy.MiniDead += AddMedicThing;
        FieldOfView.Spoted += FindedMetod;
        LandingCheck.Landing += LandAnim;
    }

    private void LandAnim()
    {
        PlayerAnimator.SetTrigger("Landing");
    }

    private void OnDisable()
    {
        MiniEnemy.MiniDead -= AddMedicThing;
        FieldOfView.Spoted -= FindedMetod;
    }
    protected void AddMedicThing()
    {
        Medicine++;
    }
    protected void FindedMetod()
    {
        Finded = true;
    }
    public void HideMech()
    {
        int i ;
        isHide = !isHide;
        if(_spriteRender.color.a == 0)
        {
            _spriteRender.color = new Color(_spriteRender.color.r, _spriteRender.color.g, _spriteRender.color.b,Mathf.Lerp(1, 0, TimeHide * Time.deltaTime));
        }
        else
        {
            _spriteRender.color = new Color(_spriteRender.color.r, _spriteRender.color.g, _spriteRender.color.b, Mathf.Lerp(0, 1, TimeHide * Time.deltaTime));
        }
    }
    
}
