using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spear : MonoBehaviour
{
    public Rigidbody rb;

    public int waitDeath;

    bool hasCollided = false;

    public Collider collided;

    private void Start()
    {
        StartCoroutine(Death());
        hasCollided = false;
    }

    private void Update()
    {
        hasCollided = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (hasCollided)
        {
            return;
        }
        else
        {

            if (other.tag == "Player")
            {
                hasCollided = true;
                other.gameObject.GetComponent<Movement>().reduceJumps(1);
                rb.isKinematic = true;
                transform.SetParent(other.transform, true);

                transform.position = other.ClosestPoint(transform.position);
                collided.enabled = false;

            }
            else if (other.tag == "Boundary")
            {
                Destroy(gameObject);
            }
        }

        
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(waitDeath);
        Destroy(gameObject);
    }
}
