using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer MyMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;



    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))

        {
            LoadVolume();

        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();

        }
    }



    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        MyMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }


    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        MyMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        SetSFXVolume();
    }


}
