using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private int NumberScene;
    public void Play(string NameScene)
    {
        SceneManager.LoadScene(NameScene);
    }
}
