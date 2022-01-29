using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    void Start()
    {
        
    }

    public void SetLevel (float sliderValue)
    {
        audioMixer.SetFloat("Master_Volume", Mathf.Log10(sliderValue) * 20);
    }
}
