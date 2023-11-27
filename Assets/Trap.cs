using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ���������� �� ������ � ����� "Player"
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            // ������� ������
            player.Dead();
        }
    }
}
