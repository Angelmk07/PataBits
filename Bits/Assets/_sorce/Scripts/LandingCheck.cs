using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingCheck : MonoBehaviour
{
    public static Action Landing;
    [SerializeField]
    LayerMask Ground;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Utils.LayerMaskUtil.ContainsLayer(Ground, collision.gameObject))
        {
            Landing?.Invoke();
        }
    }
}
