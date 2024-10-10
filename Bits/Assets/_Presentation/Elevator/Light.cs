using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Light : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve AnimationCurve;
    [SerializeField]
    private SpriteRenderer Spr;
    private float time;
    private float timeBit = 0.666f;
    private void Start()
    {
        Spr.color = new Color(Spr.color.r, Spr.color.g, Spr.color.b, AnimationCurve.Evaluate(time));
    }
    void Update()
    {
        if (time >= timeBit)
        {
            Spr.color = new Color(Spr.color.r, Spr.color.g, Spr.color.b, AnimationCurve.Evaluate(time));
            time -= Time.deltaTime;
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
