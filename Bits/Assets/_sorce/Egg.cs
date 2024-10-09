
using System.Collections;
using UnityEngine;


public class Egg : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private GameObject _point;
    [SerializeField]
    private PlayerS _player;
    private void OnMouseDown()
    {
        StartCoroutine(Skip());

    }
    IEnumerator Skip()
    {
        _particleSystem.Emit(30);
        yield return new WaitForSeconds(1);
        _player.Player.transform.position = _point.transform.position;
    }
}
