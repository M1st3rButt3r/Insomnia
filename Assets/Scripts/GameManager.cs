using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool startedRecording;
    public bool startedReplay;

    [HideInInspector]
    public bool secondRun;
    [HideInInspector]
    public GameObject secondPlayer;

    public GameObject player;
    public GameObject playerPrefab;

    public List<IResettable> Resettables = new List<IResettable>();
    
    public CinemachineVirtualCamera Camera;

    public GameObject deathUI;
    public TMP_Text deathUIModifyableText;

    private Vector3 startPositionPlayer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startPositionPlayer = player.transform.position;

        PlayerInput.Instance.Input += () =>
        { 
            if (!startedRecording)
            {
                player.GetComponent<MovementRecorder>().StartRecording();
                startedRecording = true;
            }
        };

        PlayerInput.Instance.Restart += Restart;
        PlayerInput.Instance.RestartHold += RestartFull;
    }     

    public void Finish()
    {
        ResetAllObjects();
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
        player.transform.position = startPositionPlayer;
        player.GetComponent<MovementRecorder>().StopRecording();
        PlayerInput.Instance.Input += () =>
        {
            if (!startedReplay)
            {
                player.GetComponent<MovementRecorder>().StartReplay();
                startedReplay = true;
            }
        };
        player.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, .6f);
        secondPlayer = Instantiate(playerPrefab);
        secondPlayer.transform.position = startPositionPlayer;
        Camera.Follow = secondPlayer.transform;
    }
    
    private void FinishSecondRun()
    {
        if (BedInteraction.Instance.playersInCollider == 2)
        {
            if (SceneManager.GetAllScenes().Length <= SceneManager.GetActiveScene().buildIndex + 1)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        }
            
    }

    public void Die(string reason)
    {
        PlayerController.playerController.canMove = false;
        deathUIModifyableText.text = reason;
        deathUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        PlayerController.playerController.canMove = true;
        deathUI.SetActive(false);
        ResetAllObjects();
        if (secondRun)
        {
            RestartSecond();
        }
        else
        {
            RestartFull();
        }
    }

    private void RestartSecond()
    {
        startedReplay = false;
        player.GetComponent<MovementRecorder>().StopReplay();
        player.transform.position = startPositionPlayer;
        secondPlayer.transform.position = startPositionPlayer;
    }

    public void RestartFull()
    {
        ResetAllObjects();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddToResettables(IResettable resettable)
    {
        Resettables.Add(resettable);
    }

    private void ResetAllObjects()
    {
        foreach (IResettable res in Resettables)
        {
            res.ResetAsset();
        }
    }
}
