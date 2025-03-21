using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Source ---")]
    [SerializeField] private AudioSource musicSource; 
    [SerializeField] private AudioSource SFXSource; 

    [Header("--- Audio Clip ---")]
    public AudioClip background;  
    public AudioClip death;       
    public AudioClip checkpoint; 
    public AudioClip nullback;   
    public AudioClip portalIn;   
    public AudioClip portalOut;  

    private void Start()
    {
        
        musicSource.clip = background;
        musicSource.Play();
    }

    
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}