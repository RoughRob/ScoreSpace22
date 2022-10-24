using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAngels : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            Destroy(gameObject);
        }

    }
}
