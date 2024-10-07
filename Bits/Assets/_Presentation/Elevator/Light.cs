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
    void Update()
    {
        time += Time.deltaTime;
        Spr.color = new Color(Spr.color.r, Spr.color.g, Spr.color.b, AnimationCurve.Evaluate(time));
    }
}
