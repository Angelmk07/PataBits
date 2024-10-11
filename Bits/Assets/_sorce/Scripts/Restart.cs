using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                RestartGame();
            }
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
