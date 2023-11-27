using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float _speed;

    private Camera _camera;
    private float _startPositionX;
    private float _startPositionY;

    private void Start()
    {
        _camera = Camera.main;
        _startPositionX = transform.position.x;
        _startPositionY = transform.position.y;
    }

    private void Update()
    {
        float distanceX = _camera.transform.position.x * (1 - _speed);
        float distanceY = _camera.transform.position.y * (1 - _speed);
        transform.position = new Vector2(_startPositionX + distanceX, _startPositionY + distanceY);
    }
}
