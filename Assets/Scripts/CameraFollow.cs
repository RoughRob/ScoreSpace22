using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, target.position.y + offset, transform.position.z);

    }
}
