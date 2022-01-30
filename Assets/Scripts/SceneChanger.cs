using System;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.SceneManagement;  
public class SceneChanger: MonoBehaviour
{
    private static readonly string MainMenu = "Menu";
    private static readonly string Level = "Level";

    private SceneChanger _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void MainGameScene() {  
        SceneManager.LoadScene(MainMenu);  
    }

    public void LoadFirstLevel()
    {
        LoadLevel(1);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}  