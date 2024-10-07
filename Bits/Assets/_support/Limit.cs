
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;


    [SerializeField]
    private float _left;
    [SerializeField]
    private float _right;
    [SerializeField]
    private float _up;
    [SerializeField]
    private float _down;
    [SerializeField]

    void LateUpdate()
    {
        Vector3 newPosition = player.position + offset;
        newPosition.x = Mathf.Clamp(newPosition.x, _left, _right);
        newPosition.y = Mathf.Clamp(newPosition.y, _down, _up);
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(_left, _up),new Vector2(_left, _down));
        Gizmos.DrawLine(new Vector2(_right, _up),new Vector2(_right, _down));
    }
}