using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource Audio;
    void Start()
    {
             Audio = GetComponent<AudioSource>();
    }


    public void PlayClip(AudioClip x)
    {
        Audio.clip = x;
        Audio.Play();
    }

}
