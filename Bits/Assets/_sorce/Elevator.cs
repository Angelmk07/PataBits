using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftTrigger : MonoBehaviour
{
    private bool isPlayerInTrigger = false;
    [SerializeField]
    LayerMask LayerMask;
    private void Update()
    {
        if (isPlayerInTrigger && (Input.GetKeyDown(KeyCode.F) || (Input.GetKeyDown(KeyCode.E))))
        {
            LoadLiftScene();
        }
    }
    private void LoadLiftScene()
    {
        SceneManager.LoadScene("Elevator");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Utils.LayerMaskUtil.ContainsLayer(LayerMask,collision.gameObject))
        {
            isPlayerInTrigger = true;
        }
    }

}