using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    [Header("Meteor Shot")]
    public GameObject meteorPrefab;
    public Transform ThrowFrom;
    public float fireRate = 1f;
    float fireCountDown = 0;
    public float meteorForce = 1;

    float aimDir = 0;

    // Start is called before the first frame update
    void Start()
    {

        aimDir = Random.Range(45, -45);
        transform.rotation = Quaternion.Euler(0, 0, aimDir);


    }

    private void Update()
    {

        if (fireCountDown < 0f)
        {
            meteorShot();
            fireCountDown = 1f / fireRate;

        }

        fireCountDown -= Time.deltaTime;


    }

    void meteorShot()
    {
        aimDir = Random.Range(45, -45);
        transform.rotation = Quaternion.Euler(0, 0, aimDir);

        GameObject meteorGO = (GameObject)Instantiate(meteorPrefab, transform.position, transform.rotation);
        Rigidbody spearBody = meteorGO.GetComponent<Rigidbody>();
        spearBody.AddForce(-transform.up * meteorForce);

    }

}
