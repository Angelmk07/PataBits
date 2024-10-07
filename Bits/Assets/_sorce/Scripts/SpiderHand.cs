using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderHand : MonoBehaviour
{
    [SerializeField]
    LayerMask player;
    [SerializeField]
    private int damage =99;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utils.LayerMaskUtil.ContainsLayer(player, collision.gameObject))
            {
            collision.TryGetComponent(out PlayerS player);
            player.TakeHit(damage);
        }
    }
}
