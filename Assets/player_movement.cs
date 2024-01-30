using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private bool _isMoving = false;
    private Vector3 _targetPosition;
    public float _speed = 2f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SetTargetPosition();
        }
        if (_isMoving)
        {
            Move();
        }
    }

    private void SetTargetPosition()
    {
        if (transform.position.x > _targetPosition.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (transform.position.x < _targetPosition.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition.z = transform.position.z;

        _isMoving = true;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        if (transform.position == _targetPosition)
        {
            _isMoving = false;
        }
    }
}
