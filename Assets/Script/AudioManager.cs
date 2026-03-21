using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    void Start()
    {
        // ตั้งค่าเริ่มต้น
        musicSource.volume = musicSlider.value;
        sfxSource.volume = sfxSlider.value;

        // เพิ่ม Listener
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}