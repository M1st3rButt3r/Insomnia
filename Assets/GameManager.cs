using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool secondRun;

    public GameObject player;
    public GameObject playerPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void Finish()
    {
        if (secondRun)
        {
            FinishSecondRun();
            return;
        }
        FinishFirstRun();
    }

    private void FinishFirstRun()
    {
        secondRun = true;
        player.transform.position = Vector3.zero;
        player.GetComponent<MovementRecorder>().StopRecording();
        player.GetComponent<MovementRecorder>().StartReplay();
        player.GetComponent<SpriteRenderer>().color = Color.red;
        Instantiate(playerPrefab);
    }
    
    private void FinishSecondRun()
    {
        Debug.Log("Finished Level");
    }
}
