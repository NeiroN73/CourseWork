using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float speed;

    private void FixedUpdate()
    {
        var position = Vector3.Lerp(transform.position, target.position, speed);
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
