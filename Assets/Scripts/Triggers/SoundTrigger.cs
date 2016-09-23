using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour
{

    public AudioClip SoundToPlay;
    public float Volume;
    AudioSource Audio;
    public bool alreadyPlayed = false;

    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter()
    {
        if (!alreadyPlayed)
        {
            Audio.PlayOneShot(SoundToPlay, Volume);
            alreadyPlayed = true;

        }
    }
}
