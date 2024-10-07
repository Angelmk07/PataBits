using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deadzone : MonoBehaviour
{
    [SerializeField]
    private Image Image;

    [SerializeField]
    private float speed =0.4f;
    [SerializeField]
    private float alfa=0;
    private void OnEnable()
    {
        FieldOfView.Spoted += StartFadeImage;
        PlayerS.Dead += StartFadeImage;
    }
    private void OnDisable()
    {
        FieldOfView.Spoted -= StartFadeImage;
        PlayerS.Dead -= StartFadeImage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartFadeImage();
    }
    void StartFadeImage()
    {
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, alfa +=Mathf.Lerp(0,1,0.005f));
        Time.timeScale = 0;
        
    }
}
