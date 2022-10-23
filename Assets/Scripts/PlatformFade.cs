using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFade : MonoBehaviour
{
    public float waitTime;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Should Fade1");
            if (other.gameObject.GetComponent<Movement>().isGrouned == true)
            {
                Debug.Log("Should Fade2");
                StartCoroutine(Fade());
            }
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

}
