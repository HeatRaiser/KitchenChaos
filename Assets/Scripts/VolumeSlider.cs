using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource musicSource;
    void Start()
    {
        if (musicSource != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("volume", 1f);

            musicSource.volume = savedVolume;
            
            volumeSlider.value = savedVolume;
        }
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
            PlayerPrefs.SetFloat("volume", volume);
        }
    }
}
