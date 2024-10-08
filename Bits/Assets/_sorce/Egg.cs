
using UnityEngine;


public class Egg : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    private void OnMouseDown()
    {
        Debug.Log("Объект " + gameObject.name + " был нажат!");
        _particleSystem.Emit(30);
    }

}
