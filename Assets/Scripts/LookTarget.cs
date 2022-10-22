using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTarget : MonoBehaviour
{
    public Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
             target = other.GetComponent<Transform>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && target != null)
        {
            target = null;
        }
    }

}
