using System;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;
    
    [SerializeField] private AudioSource backgroundMusic;

    private const string PLAYER_PREFS_MUSIC_VOLUME = "BackgroundMusic";
    private const string PLAYER_PREFS_EFFECTS_VOLUME = "EffectsVolume";


    private void Start()
    {
        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        effectsSlider.onValueChanged.AddListener(ChangeEffectsVolume);
    }

    private void ChangeEffectsVolume(float volume)
    {
        SoundManager.Instance.effectsVolume = volume;
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    private void ChangeMusicVolume(float volume)
    {
        backgroundMusic.volume = volume;
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public void UpdateSliders()
    {
        musicSlider.value = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 1f);
        effectsSlider.value = PlayerPrefs.GetFloat(PLAYER_PREFS_EFFECTS_VOLUME, 1f);
    }

    
    // [SerializeField] private Slider volumeSlider;
    // [SerializeField] private AudioSource musicSource;
    // void Start()
    // {
    //     if (musicSource != null)
    //     {
    //         float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
    //
    //         musicSource.volume = savedVolume;
    //         
    //         volumeSlider.value = savedVolume;
    //     }
    //     if (volumeSlider != null)
    //     {
    //         volumeSlider.onValueChanged.AddListener(SetVolume);
    //     }
    // }
    //
    // public void SetVolume(float volume)
    // {
    //     if (musicSource != null)
    //     {
    //         musicSource.volume = volume;
    //         PlayerPrefs.SetFloat("volume", volume);
    //     }
    // }
}
