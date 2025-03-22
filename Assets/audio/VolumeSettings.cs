
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if (musicSlider == null)
        {
            Debug.LogError("Music Slider is not assigned in VolumeSettings script!");
            return;
        }

        SetMusicVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        if (volume <= 0) // Kad išvengti log(0) klaidos
            volume = 0.0001f;

        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }
}