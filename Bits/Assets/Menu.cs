using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private int NumberScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(NumberScene);
        PlayerPrefs.SetInt("HaveSave", 1);
        PlayerPrefs.SetFloat("PointX", 20.25f);
        PlayerPrefs.SetFloat("PointY", -0.32f);
    }

    public void Play(string NameScene)
    {
        SceneManager.LoadScene(NameScene);
    }
}
