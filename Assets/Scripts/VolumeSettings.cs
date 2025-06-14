using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _mainMixer;
    [SerializeField] private AudioMixer _sfxMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;


    private void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("SfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSfxVolume();
        }
    }

    public void SetSfxVolume()
    {
        float sfxVolume = _sfxSlider.value;
        _sfxMixer.SetFloat("Sfx", Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("SfxVolume",sfxVolume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume()
    {
        float musicVolume = _musicSlider.value;
        _mainMixer.SetFloat("Music",Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("musivVolume", musicVolume);
        PlayerPrefs.Save();
    }

    private void LoadVolume()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
        SetMusicVolume();
        SetSfxVolume();
    }

}
