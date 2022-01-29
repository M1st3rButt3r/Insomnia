using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public enum PPProfile
{
    Day,
    Night
}

public class PostEffects : MonoBehaviour
{
    public static PostEffects Instance;
    
    public float timeToFade;
    public float fadeStarted;
    
    public PPProfile activeProfile;

    public Volume day;
    public Volume night;

    private void Awake()
    {
        Instance = this;
        fadeStarted = -timeToFade;
    }

    public void Update()
    {
        switch (activeProfile)
        {
            case PPProfile.Day:
                day.weight = Fade();
                night.weight = 1 - Fade();
                break;
            case PPProfile.Night:
                night.weight = Fade();
                day.weight = 1 - Fade();
                break;
        }
    }

    public void ActivateProfile(PPProfile profile)
    {
        activeProfile = profile;
        StartFade();
    }

    public void StartFade()
    {
        fadeStarted = Time.time;
    }

    private float Fade()
    {
        return Mathf.Clamp((Time.time - fadeStarted) / timeToFade, 0, 1);
    }
}
