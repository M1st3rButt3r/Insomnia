using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioMixer audioMixer;

    public GameObject _camera;

    public AudioClip menuclick1;

    public static string SFX = "SFX";
    public static string BGM = "BGM";
    public static string Menu = "Menu";

    private void Awake()
    {
        Instance = this;       
    }

    public void SetLevel (float sliderValue)
    {
        audioMixer.SetFloat("Master_Volume", Mathf.Log10(sliderValue) * 20);
    }
    
    public void SetBGMLevel (float sliderValue)
    {
        audioMixer.SetFloat("BGM_Volume", Mathf.Log10(sliderValue) * 20);
    }

    public void PlayMenuClick()
    {
        PlayBGM(menuclick1, Menu);
    }

    public AudioSource PlayBGM(AudioClip clip, string audioMixerGroup, bool loop = false, float spatialBlend = 0, float volume = 1)
    {
        if(clip == null)return null;
        return PlayOnGO(clip, _camera, audioMixerGroup, loop, spatialBlend, volume);
    }

    public AudioSource PlayOnGO(AudioClip clip, GameObject GO, string audioMixerGroup, bool loop = false, float spatialBlend = 0, float volume = 1)
    {
        if(clip == null || GO == null)return null;

        AudioSource source = GO.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = loop;
        source.spatialBlend = spatialBlend;
        source.volume = volume;
        AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(audioMixerGroup);
        if (groups.Length > 0)
        {
            source.outputAudioMixerGroup = groups[0];
        }

        source.Play();
        if (!loop)
        {
            StartCoroutine(DestroySource(clip.length, source));
        }

        return source;
    }

    IEnumerator DestroySource(float time, AudioSource source)
    {
        yield return new WaitForSeconds(time);
        Destroy(source);
    }
}
