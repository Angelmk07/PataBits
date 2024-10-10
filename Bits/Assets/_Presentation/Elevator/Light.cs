using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Light : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer Spr;
    private float index =1;
    private float LastVal =0;
    [SerializeField]
    private PostProcessVolume _postProcessVolume;
    private Vignette _vignette;
    private void Start()
    {
        _postProcessVolume.profile.TryGetSettings(out Vignette v);
        _vignette = v;
        
    }
    void Update()
    {
        if(_vignette.intensity.value != LastVal)
        {
            Spr.color = new Color(Spr.color.r, Spr.color.g, Spr.color.b, _vignette.intensity.value);
            LastVal = _vignette.intensity.value;
        }
    }
}
