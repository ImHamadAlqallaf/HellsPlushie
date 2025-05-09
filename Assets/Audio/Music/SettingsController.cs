using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Toggle muteMusicToggle;
    public Image brightnessOverlay;
    public Slider brightnessSlider;

    void Start()
    {
        SetMusicVolume(musicSlider.value);
    }

    public void SetMusicVolume(float value)
    {
        float dB = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
        audioMixer.SetFloat("MusicVolume", dB);
    }

    public void ToggleMusic(bool isMuted)
    {
        if (isMuted)
        {
            audioMixer.SetFloat("MusicVolume", -80f); // effectively mute
        }
        else
        {
            SetMusicVolume(musicSlider.value); // restore current slider value
        }
    }

    public void SetBrightness(float value)
    {
        if (brightnessOverlay != null)
        {
            Color c = brightnessOverlay.color;
            c.a = 1 - value;
            brightnessOverlay.color = c;
        }
    }
}
