using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            int tempJumps = other.gameObject.GetComponent<Movement>().jumps;
            other.gameObject.GetComponent<Movement>().reduceJumps(tempJumps);

            Destroy(gameObject);
        }
        else if (other.tag == "Boundary")
        {
            Destroy(gameObject);
        }

    }

}
