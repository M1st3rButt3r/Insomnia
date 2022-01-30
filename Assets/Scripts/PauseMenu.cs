using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;

    public GameObject pauseMenuUI;


    private void Start()
    {
        PlayerInput.Instance.Pause += TogglePause;
    }

    private void TogglePause()
    {
        if (Paused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
            
    }
        

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        SoundManager.Instance.SetBGMLevel(1f);
        
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        SoundManager.Instance.SetBGMLevel(0.5f);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
