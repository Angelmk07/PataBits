using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public static Action hit;
    [SerializeField]
    private int Hist = 0;
    public void TakeHit(int val)
    {
        Hist += val;
        hit?.Invoke();
    }
}
