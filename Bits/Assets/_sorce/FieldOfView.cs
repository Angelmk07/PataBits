using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    static public Action Spoted;

    public float viewAngle = 90f; 
    public float viewDistance = 5f; 
    public int rayCount = 30;

    [SerializeField]
    private LayerMask Player;
    

    private Mesh mesh;
    private MeshFilter meshFilter;
    private Vector3[] vertices;
    private int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = new Color(1, 1, 1, 0.3f); 
    }

    void Update()
    {
        DrawFieldOfView();
    }

    void DrawFieldOfView()
    {
        float angleStep = viewAngle / rayCount; 
        vertices = new Vector3[rayCount + 2]; 
        triangles = new int[rayCount * 3]; 

        vertices[0] = Vector3.zero;

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + angleStep * i; 
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)); 
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, viewDistance); 

            Vector3 hitPoint = hit ? hit.point : (Vector3)transform.position + (Vector3)direction * viewDistance;
            Debug.DrawLine(transform.position, hitPoint, hit ? Color.red : Color.green);

            vertices[i + 1] = transform.InverseTransformPoint(hitPoint); 

            if (i < rayCount)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1; 
                triangles[i * 3 + 2] = i + 2; 
            }
            if (hit.collider != null)
            {
                if (Utils.LayerMaskUtil.ContainsLayer(Player, hit.collider.gameObject))
                {
                    if (!hit.collider.gameObject.GetComponent<PlayerS>().isHide)
                    {
                        Spoted?.Invoke();
                    }

                }
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}