using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f; // �������� �������� ���������
    public float distance = 5f; // ����������, �� ������� ��������� ����� ���������

    private Vector3 startPos;
    private float currentDistance;
    private int direction = 1;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // ������������ ������� ���������� ����� ��������� �������� � ������� �������� ���������
        currentDistance = Mathf.Abs(transform.position.x - startPos.x);

        // ���� ������� ���������� ������ ��� ����� ��������� ����������, ������ ����������� �������� ���������
        if (currentDistance >= distance)
        {
            direction *= -1;
        }

        // ��������� ����� ������� ��������� � ������ ����������� � ��������
        float moveHorizontal = direction * speed * Time.deltaTime;
        transform.Translate(new Vector3(moveHorizontal, 0f, 0f));
    }
}
