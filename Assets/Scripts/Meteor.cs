using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    AudioSource Audio;
    public AudioClip clip;

    private void Start()
    {
        Audio = GetComponent<AudioSource>();    
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            int tempJumps = other.gameObject.GetComponent<Movement>().jumps;
            other.gameObject.GetComponent<Movement>().reduceJumps(tempJumps);


            other.gameObject.GetComponent<PlayAudio>().PlayClip(clip);
            Destroy(gameObject);


        }
        else if (other.tag == "Boundary")
        {
            Destroy(gameObject);
        }

    }

}
