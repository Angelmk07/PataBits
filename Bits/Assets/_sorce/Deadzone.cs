using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deadzone : MonoBehaviour
{
    [SerializeField]
    private Image Image;
    [SerializeField]
    private GameObject EndDead;
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
    private void OnBecameInvisible()
    {
        StartFadeImage();
    }
    void StartFadeImage()
    {
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 1);
        EndDead.SetActive(true);
        Time.timeScale = 0;
        
    }
}
