using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private bool _isMoving = false;
    private Vector2 _targetPosition;
    public float _speed = 2f;

    [SerializeField] private float targetRadius = 0.25f;

    
    [SerializeField] LayerMask wallLayer;

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
        

        Vector2 dir =  (_targetPosition - (Vector2)transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(
            origin: transform.position,
            direction: dir,
            distance: Vector2.Distance(transform.position, _targetPosition),
            layerMask: wallLayer);

        if (hit)
        {
            _targetPosition = hit.point;
            
        }
        
        _isMoving = true;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, _targetPosition) < 0.15f)
        {
            _isMoving = false;
        }
    }

    
}
