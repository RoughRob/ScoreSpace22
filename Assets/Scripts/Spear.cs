using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spear : MonoBehaviour
{

    public Transform stuckTrans;
    public Rigidbody rb;
    Transform struckTrans;

    public int waitDeath;


    private void Start()
    {
        StartCoroutine(Death());
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            rb.isKinematic = true;
            transform.SetParent(other.transform, true);

            transform.position = other.ClosestPoint(transform.position);

        }
    }


    IEnumerator Death()
    {
        yield return new WaitForSeconds(waitDeath);
        Destroy(gameObject);
    }
}
