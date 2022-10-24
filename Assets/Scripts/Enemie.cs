using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemie : MonoBehaviour
{

    public LookTarget lookTarget;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("Spear Throwing")]
    public GameObject SpearPrefab;
    public Transform ThrowFrom;
    public float fireRate = 1f;
    float fireCountDown = 0;
    public float spearForce = 1;

    public AudioClip clip;

    // Update is called once per frame
    void Update()
    {
        if (lookTarget.target != null)
        {
            // Vector3 lookDir = new Vector3(target.position.x, target.position.y, target.position.z);
            // transform.rotation = Quaternion.LookRotation(lookDir);

            Vector2 LookDir = lookTarget.target.position - transform.position;
            float targetAngle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle + 90, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);

            if (fireCountDown < 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;

            }

            fireCountDown -= Time.deltaTime;

        }
        else
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, 0, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
        }


    }

    void Shoot()
    {
        GameObject spearGO = (GameObject)Instantiate(SpearPrefab, ThrowFrom.position, ThrowFrom.rotation);
        Rigidbody spearBody = spearGO.GetComponent<Rigidbody>();
        spearBody.AddForce(-ThrowFrom.up * spearForce);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int tempJumps = other.gameObject.GetComponent<Movement>().jumps;
            other.gameObject.GetComponent<Movement>().reduceJumps(tempJumps);

            other.gameObject.GetComponent<PlayAudio>().PlayClip(clip);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }


}
