using System;
using System.Collections;
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

    public bool recording;
    public float recordingStarted;
    public bool replay;
    public float replayStarted;

    public Action<Vector2> Move;
    
    private void Start()
    {
        PlayerInput.Instance.Move += RecordMove;
        PlayerInput.Instance.JumpStart += RecordJumpStart;
        PlayerInput.Instance.JumpEnd += RecordJumpEnd;
    }

    private void Update()
    {
        if(replay) Replay();
    }

    public void StartRecording()
    {
        recording = true;
        recordingStarted = Time.time;
    }
    
    public void StopRecording()
    {
        recording = false;
    }

    public void StartReplay()
    {
        replay = true;
        replayStarted = Time.time;
    }

    public float GetRecordingTime()
    {
        return Time.time - recordingStarted;
    }

    private void RecordMove()
    {
        if(recording)
            MovementActions.Add(GetRecordingTime(), new MovementAction<Vector2>(MovementActionType.Move, PlayerInput.Instance.moveInput));
    }

    private void RecordJumpStart()
    {
        if(recording)
            MovementActions.Add(GetRecordingTime(), new MovementAction(MovementActionType.JumpStart));
    }
    
    private void RecordJumpEnd()
    {
        if(recording)
            MovementActions.Add(GetRecordingTime(), new MovementAction(MovementActionType.JumpEnd));
    }

    private void Replay()
    {
        for (int i = 0; i < MovementActions.Count; i++)
        {
            if (replayStarted + MovementActions.Keys.ToList()[i] > Time.time) return;
            switch (MovementActions.Values.ToList()[i].ActionType)
            {
                case MovementActionType.Move:
                    ReplayMove((MovementAction<Vector2>)MovementActions.Values.ToList()[i]);
                    break;
            }
        }
    }

    private void ReplayMove(MovementAction<Vector2> action)
    {
        Move.Invoke(action.Value);
    }
}
