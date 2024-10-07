using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    PlayerS player;
    [SerializeField]
    LayerMask LayerMask;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("HaveSave") == 1)
        {
            player.transform.position = new Vector2(PlayerPrefs.GetFloat("PointX"), PlayerPrefs.GetFloat("PointX"));
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Utils.LayerMaskUtil.ContainsLayer(LayerMask, collision.gameObject))
        {
            SetPoint();
        }
    }
    void SetPoint()
    {
        PlayerPrefs.SetInt("HaveSave", 1);
        PlayerPrefs.SetFloat("PointX", player.Player.transform.position.x);
        PlayerPrefs.SetFloat("PointY", player.Player.transform.position.y);
    }
    
}
