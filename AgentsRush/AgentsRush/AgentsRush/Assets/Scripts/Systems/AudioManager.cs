using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource soundEffectAudioSource;
    public AudioSource mainMusicAudioSource;
    [Header("Audio Clips")]
    public AudioClip inGameMusic;
    public AudioClip defeatAllEnemiesClip;
    public AudioClip rageSound;
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

        soundEffectAudioSource = this.GetComponent<AudioSource>();
        soundEffectAudioSource.loop = false;
    }

    //play start method
    public void PlayStart()
    {
        UiManager.instance.ShowWaveText(0, "Defeat All The Enemies to Proceed", 3f);
        PlaySoundEffect(defeatAllEnemiesClip);
        Invoke(nameof(PlayMusic), 3f);
    }

    //play music and set up volume
    public void PlayMusic()
    {
        mainMusicAudioSource.clip = inGameMusic;
        mainMusicAudioSource.Play();
        UiManager.instance.HideWaveText();
        mainMusicAudioSource.loop = true;
        SetVolume(1f);
    }

    //play sound effect based on a string name
    public void PlaySoundEffect(AudioClip clip)
    {
        soundEffectAudioSource.clip = clip;
        soundEffectAudioSource.Play();
    }
    public void PauseMainMusic()
    {
        mainMusicAudioSource.Pause();
    }
    public void RageModeMusic()
    {
        StartCoroutine(RageModeEnumerator());
    }
    private IEnumerator RageModeEnumerator()
    {
        //pause the main music 
        mainMusicAudioSource.Pause();

        //unpause the audio source after 9 seconds
        yield return new WaitForSeconds(9f);
        mainMusicAudioSource.UnPause();
}
    //set the volume
    public void SetVolume(float volume)
    {
        mainMusicAudioSource.volume = volume;
    }
}
