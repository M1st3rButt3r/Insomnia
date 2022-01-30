using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    [HideInInspector]
    public Vector2 moveInput;
    public Action Move;
    public Action JumpStart;
    public Action JumpEnd;
    public Action Interact;
    public Action Test;
    public Action Input;
    public Action Restart;
    public Action RestartHold;
    public Action Pause;

    private void Awake()
    {
        Instance = this;
    }

    private void OnPause()
    {
        Pause?.Invoke();
    }

    private void OnMove(InputValue value)
    {
        Input?.Invoke();
        moveInput = value.Get<Vector2>();
        Move?.Invoke();
    }

    private void OnJumpStart()
    {
        Input?.Invoke();
        JumpStart?.Invoke();
    }

    private void OnJumpEnd()
    {
        Input?.Invoke();
        JumpEnd?.Invoke();
    }

    private void OnTest()
    {
        Input?.Invoke();
        Test?.Invoke();
    }

    private void OnInteract()
    {
        Input?.Invoke();
        Interact?.Invoke();
    }

    private void OnRestart()
    {
        Restart?.Invoke();
    }

    private void OnRestartHold()
    {
        RestartHold?.Invoke();
    }
}
