using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewAngle = 90f; // Угол обзора
    public float viewDistance = 5f; // Дальность видимости
    public int rayCount = 30; // Количество лучей
    private LineRenderer lineRenderer; // Компонент LineRenderer

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = rayCount + 2; // +2 для начальной точки (позиция игрока) и замыкания
        lineRenderer.startWidth = 0.1f; // Ширина линии
        lineRenderer.endWidth = 0.1f; // Ширина линии
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Устанавливаем материал
        lineRenderer.startColor = Color.white; // Начальный цвет
        lineRenderer.endColor = Color.white; // Конечный цвет
    }
    void Update()
    {
        DrawFieldOfView();
    }
    void DrawFieldOfView()
    {
        float angleStep = viewAngle / rayCount; // Шаг угла между лучами
        Vector3[] points = new Vector3[rayCount + 2]; // Массив для хранения точек (включая начальную точку и замыкание)
        points[0] = transform.position;
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + angleStep * i; // Вычисляем угол
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)); // Направление луча
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, viewDistance); // Проверяем столкновение
            Vector3 hitPoint = hit ? hit.point : (Vector3)transform.position + (Vector3)direction * viewDistance;
            points[i + 1] = hitPoint;
            Debug.DrawLine(transform.position, hitPoint, hit ? Color.red : Color.green);
        }
        points[rayCount + 1] = points[0]; 
        lineRenderer.SetPositions(points);
    }
}