
using UnityEngine;


public class Egg : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    private void OnMouseDown()
    {
        Debug.Log("������ " + gameObject.name + " ��� �����!");
        _particleSystem.Emit(30);
    }

}
