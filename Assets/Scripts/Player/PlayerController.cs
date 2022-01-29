using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public int speed;
    public float raycastLength;
    public LayerMask ground;
    public float jumpForce;
    public float koyoteTime;
    public float jumpTime;

    private Rigidbody2D _rb;
    private bool _grounded;
    private float _lastGrounded;
    private bool _isJumping;
    private float _jumpUntil;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerInput.Instance.JumpStart += JumpStart;
        PlayerInput.Instance.JumpEnd += JumpEnd;
    }

    void Update()
    {
        Grounded();
        Move();
        if(_isJumping) Jumping();
    }

    private void Move()
    {
        _rb.velocity = new Vector2(PlayerInput.Instance.moveInput.x * speed * Time.deltaTime, _rb.velocity.y);
    }

    private void Grounded()
    {
        Vector2 origin1 = transform.position - new Vector3(0, transform.localScale.y/2);
        Vector2 origin2 = transform.position - new Vector3(-transform.localScale.x, transform.localScale.y)/2;
        Vector2 origin3 = transform.position - transform.localScale /2;

        bool origin1bool = Physics2D.Raycast(origin1, Vector2.down, raycastLength, ground);
        bool origin2bool = Physics2D.Raycast(origin2, Vector2.down, raycastLength, ground);
        bool origin3bool = Physics2D.Raycast(origin3, Vector2.down, raycastLength, ground);
        _grounded =  origin1bool || origin2bool || origin3bool;
        if (_grounded) _lastGrounded = Time.time;
    }

    private void JumpStart()
    {
        if(!_grounded && _lastGrounded + koyoteTime <= Time.time) return;
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector2.up * jumpForce);
        _isJumping = true;
        _jumpUntil = Time.time + jumpTime;
    }

    private void Jumping()
    {
        if (_jumpUntil < Time.time)
        {
            _isJumping = false;
            return;
        }

        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce * Time.deltaTime);
    }

    private void JumpEnd()
    {
        _isJumping = false;
    }
}
