using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public static player_movement instance; 

    private bool _isMoving = false;
    private Vector2 _targetPosition;
    public float _speed = 0.1f;

    [SerializeField] private float targetRadius = 0.25f;

    public Animator anim;
    private Rigidbody2D rb;

    
    [SerializeField] LayerMask wallLayer;

    public bool IsMoving
    {
        get => _isMoving;
        set => _isMoving = value;
    }

    public Vector2 TargetPosition
    {
        get => _targetPosition;
        set => _targetPosition = value;
    }

    private void Awake() {
        if (!instance)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && GameStateController.CanAct)
        {
            SetTargetPosition();
        }
        
    }

    private void FixedUpdate()
    {
        if (IsMoving && GameStateController.CanAct)
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
        
        IsMoving = true;
    }

    private void Move()
    {
       // transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, _targetPosition) < targetRadius)
        {
            IsMoving = false;
        }

        Vector2 velocity  = (_targetPosition - rb.position).normalized * _speed;
        rb.MovePosition(rb.position + velocity);

       // Debug.Log(velocity);
        anim.SetFloat("directionX", velocity.x);
        anim.SetFloat("directionY", velocity.y);
        anim.SetBool("_isMoving", IsMoving);

    }

    
}
