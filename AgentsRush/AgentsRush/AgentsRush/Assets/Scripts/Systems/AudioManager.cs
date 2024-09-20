using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioClip inGameMusic;

    private void Awake()
    {
        //singleton pattern
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayStart();
    }

    //play start method
    public void PlayStart()
    {
        UiManager.instance.ShowWaveText(0, "Defeat All The Enemies to Proceed", 3f);
        PlaySoundEffect("Defeat All The Enemies to Proceed");
        Invoke(nameof(PlayMusic), 3f);
    }

    //play music and set up volume
    public void PlayMusic()
    {
        SetVolume(0.5f);
        audioSource.clip = inGameMusic;
        audioSource.Play();
        UiManager.instance.HideWaveText();
    }

    //play sound effect based on a string name
    public void PlaySoundEffect(string soundName)
    {

    }
    //set the volume
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
