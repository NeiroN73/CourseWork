using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f; // Скорость движения платформы
    public float distance = 5f; // Расстояние, на которое платформа будет двигаться

    private Vector3 startPos;
    private float currentDistance;
    private int direction = 1;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // Рассчитываем текущее расстояние между начальной позицией и текущей позицией платформы
        currentDistance = Mathf.Abs(transform.position.x - startPos.x);

        // Если текущее расстояние больше или равно заданному расстоянию, меняем направление движения платформы
        if (currentDistance >= distance)
        {
            direction *= -1;
        }

        // Вычисляем новую позицию платформы с учетом направления и скорости
        float moveHorizontal = direction * speed * Time.deltaTime;
        transform.Translate(new Vector3(moveHorizontal, 0f, 0f));
    }
}
