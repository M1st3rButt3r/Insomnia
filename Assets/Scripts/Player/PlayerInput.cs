using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    [HideInInspector]
    public Vector2 moveInput;
    public Action jump;
    public float jumping;
    
    private void Awake()
    {
        Instance = this;
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJumpStart()
    {
        jump?.Invoke();
        jumping = Time.time;
    }

    private void OnJumpEnd()
    {
        jumping = 0f;
    }
}
