using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //singleton pattern
    public static AudioManager instance;

    private AudioSource aud;

    [Header("Dialogue Clips")]

    //dialogue clips
    [SerializeField] private AudioClip[] dialogueClips;

    //singleton implementation
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        //get the audiosource 
        aud = GetComponent<AudioSource>();
    }
    //play sound effect based on an audio clip
    public void PlaySoundEffect(string clipName)
    {
        AudioClip clipToPlay = null;

        //checks in the list if the audio clip matches the name we pass on the function
        foreach (AudioClip clip in dialogueClips)
        {
            if (clip.name == clipName)
            {
                clipToPlay = clip;
                break;
            }
        }

        //if clip is found, play it
        if (clipToPlay != null)
        {
            aud.clip = clipToPlay;
            aud.Play();
        }
        else
        {
            Debug.Log("No audio clip is found");
        }
    }
}
