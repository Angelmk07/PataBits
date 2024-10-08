using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Nothinng : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume PostProcessVolume;
    private Vignette Vignette;
    [SerializeField]
    private float Speed;

    void Start()
    {
        PostProcessVolume.profile.TryGetSettings(out Vignette);
    }

    void Update()
    {
        if (Vignette.intensity <= 0.6f)
        {
            Vignette.intensity.value += Time.deltaTime * Speed;
        }
    }
}
