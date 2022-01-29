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

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (finnished)
            return;
        float t = Time.time - startTime;
        
        timerText.text = TimeSpan.FromSeconds(t).ToString("mm':'ss':'ff");
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
