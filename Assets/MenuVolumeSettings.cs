using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuVolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMenuMixer;
    [SerializeField] private Slider musicMenuSlider;

    private void Start_menu()
    {
        if (musicMenuSlider == null)
        {
            Debug.LogError("Music Slider is not assigned in VolumeSettings script!");
            return;
        }

        SetMenuMusicVolume();
    }

    public void SetMenuMusicVolume()
    {
        float volume = musicMenuSlider.value;
        if (volume <= 0) // Kad išvengti log(0) klaidos
            volume = 0.0001f;

        myMenuMixer.SetFloat("menu", Mathf.Log10(volume) * 20);
    }
}