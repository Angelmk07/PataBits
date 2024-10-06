using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerS : MonoBehaviour
{
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
    public int power { private set; get; }
    private void OnEnable()
    {
        MiniEnemy.MiniDead += AddMedicThing;
    }
    private void OnDisable()
    {
        MiniEnemy.MiniDead -= AddMedicThing;
    }
    protected void AddMedicThing()
    {
        Medicine++;
    }
}
