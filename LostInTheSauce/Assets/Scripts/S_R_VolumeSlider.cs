using UnityEngine;
using UnityEngine.Audio;

public class S_R_VolumeSlider : MonoBehaviour
{
   [SerializeField] private AudioMixer AudioMixer;
    public void MasterVolume(float sliderValue)
    {
        AudioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue)*20); 
    }
    public void EffectsVolume(float sliderValue)
    {
        AudioMixer.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void MusicVolume(float sliderValue)
    {
        AudioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void AmbienceVolume(float sliderValue)
    {
        AudioMixer.SetFloat("AmbienceVolume", Mathf.Log10(sliderValue) * 20);
    }
}
