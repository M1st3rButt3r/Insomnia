using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;

    private float startTime;
    private bool finnished = false;
    private List<float> results = new List<float>();
    
    void Start()
    {
        startTime = Time.time;
    }
    
    void Update()
    {
        if (finnished)
            return;
        float t = Time.time - startTime;
        
        timerText.text = TimeSpan.FromSeconds(t).ToString("m'.'ss");
    }

    private void Finnished()
    {
        finnished = true;
        timerText.color = Color.red;
    }

    private void FinnishedList()
    {
        Finnished();
        results.Add(Time.time - startTime);
    }
}
