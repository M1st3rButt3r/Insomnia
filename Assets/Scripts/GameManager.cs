using System.Runtime.CompilerServices;
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
    public CinemachineVirtualCamera Camera;

    public GameObject deathUI;
    public TMP_Text deathUIModifyableText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {     
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
        PlayerInput.Instance.Input += () =>
        {
            if (!startedReplay)
            {
                player.GetComponent<MovementRecorder>().StartReplay();
                startedReplay = true;
            }
        };
        player.GetComponent<SpriteRenderer>().color = Color.red;
        secondPlayer = Instantiate(playerPrefab);
        Camera.Follow = secondPlayer.transform;
    }
    
    private void FinishSecondRun()
    {
        Debug.Log("Finished Level");
    }

    public void Die(string reason)
    {
        PlayerController.playerController.canMove = false;
        deathUIModifyableText.text = reason;
        deathUI.SetActive(true);
    }

    public void Restart()
    {
        PlayerController.playerController.canMove = true;
        deathUI.SetActive(false);
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
        player.transform.position = Vector3.zero;
        secondPlayer.transform.position = Vector3.zero;
        MotherMushroom.ResetAll();
    }

    public void RestartFull()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
