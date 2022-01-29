using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MovementActionType
{
    Move,
    JumpStart,
    JumpEnd
}

public class MovementAction
{
    public MovementActionType ActionType;
    public bool Replayed = false;

    public MovementAction(MovementActionType actionType)
    {
        ActionType = actionType;
    }
}

public class MovementAction<T> : MovementAction
{
    public T Value;
    
    public MovementAction(MovementActionType actionType, T value) : base(actionType)
    {
        Value = value;
    }
}

public class MovementRecorder : MonoBehaviour
{
    public Dictionary<float, MovementAction> MovementActions = new Dictionary<float, MovementAction>();

    private bool recording;
    private float recordingStarted;
    private bool replay;
    private float replayStarted;
    
    private Vector2 moveInput;
    

    private PlayerController _controller;
    private void Start()
    {
        _controller = GetComponent<PlayerController>();
        PlayerInput.Instance.Move += RecordMove;
        PlayerInput.Instance.JumpStart += RecordJumpStart;
        PlayerInput.Instance.JumpEnd += RecordJumpEnd;
        StartRecording();
        PlayerInput.Instance.Test += () =>
        {
            StopRecording();
            StartReplay();
        };
    }

    private void Update()
    {
        if(replay) Replay();
        Move();
    }

    public void StartRecording()
    {
        recording = true;
        recordingStarted = Time.time;
        MovementActions = new Dictionary<float, MovementAction>();
    }
    
    public void StopRecording()
    {
        recording = false;
    }

    public void StartReplay()
    {
        ResetReplay();
        replay = true;
        replayStarted = Time.time;

        _controller.DeactivateInput();
    }
    
    public void StopReplay()
    {
        replay = false;
        _controller.ActivateInput();
    }

    public void ResetReplay()
    {
        foreach (var action in MovementActions.Values)
        {
            action.Replayed = false;
        }
    }

    public float GetRecordingTime()
    {
        return Time.time - recordingStarted;
    }

    private void RecordMove()
    {
        if (recording)
            MovementActions.Add(GetRecordingTime() + Random.Range(0.0f, 0.01f), new MovementAction<Vector2>(MovementActionType.Move, PlayerInput.Instance.moveInput));
    }

    private void RecordJumpStart()
    {
        if(recording)
            MovementActions.Add(GetRecordingTime() + Random.Range(0.0f, 0.01f), new MovementAction(MovementActionType.JumpStart));
    }
    
    private void RecordJumpEnd()
    {
        if(recording)
            MovementActions.Add(GetRecordingTime() + Random.Range(0.0f, 0.01f), new MovementAction(MovementActionType.JumpEnd));
    }

    private void Replay()
    {
        for (int i = 0; i < MovementActions.Count; i++)
        {
            if (replayStarted + MovementActions.Keys.ToList()[i] > Time.time) return;
            if(MovementActions.Values.ToList()[i].Replayed) continue;
            
            switch (MovementActions.Values.ToList()[i].ActionType)
            {
                case MovementActionType.Move:
                    ReplayMove((MovementAction<Vector2>)MovementActions.Values.ToList()[i]);
                    break;
                case MovementActionType.JumpStart:
                    ReplayJumpStart();
                    break;
                case MovementActionType.JumpEnd:
                    ReplayJumpEnd();
                    break;
            }
            MovementActions.Values.ToList()[i].Replayed = true;
        }
    }

    private void ReplayMove(MovementAction<Vector2> action)
    {
        moveInput = action.Value;
    }
    
    private void ReplayJumpStart()
    {
        _controller.JumpStart();
    }
    
    private void ReplayJumpEnd()
    {
        _controller.JumpEnd();
    }

    private void Move()
    {
        _controller.Move(moveInput);
    }
}
