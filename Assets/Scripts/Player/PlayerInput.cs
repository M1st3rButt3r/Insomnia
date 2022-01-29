using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    [HideInInspector]
    public Vector2 moveInput;
    public Action Move;
    public Action JumpStart;
    public Action JumpEnd;

    private void Awake()
    {
        Instance = this;
    }

    private void OnMove(InputValue value)
    {
        Move.Invoke();
        moveInput = value.Get<Vector2>();
    }

    private void OnJumpStart()
    {
        JumpStart?.Invoke();
    }

    private void OnJumpEnd()
    {
        JumpEnd?.Invoke();
    }
}
