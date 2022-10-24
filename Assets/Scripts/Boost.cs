using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            other.gameObject.GetComponent<PlayAudio>().PlayClip(clip);
            other.gameObject.GetComponent<Movement>().BoostJump();
            Destroy(gameObject);


        }


    }



}
