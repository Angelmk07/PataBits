using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve AnimationCurve;
    [SerializeField]
    private SpriteRenderer Spr;
    private float time;
    private float timeBit = 0.666f;
    private float timewhis = 0.666f;
    private void Start()
    {
        Spr.color = new Color(Spr.color.r, Spr.color.g, Spr.color.b, AnimationCurve.Evaluate(time));
        NewBit();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= timeBit)

            NewBit();
            Spr.color = new Color(Spr.color.r, Spr.color.g, Spr.color.b, AnimationCurve.Evaluate(time));
    }
    void NewBit()
    {
        timewhis = time + timeBit;
    }
}
