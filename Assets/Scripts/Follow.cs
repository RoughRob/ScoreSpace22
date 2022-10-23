using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 followDis;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x + followDis.x, target.position.y + followDis.y,target.position.z + +followDis.z);

    }
}
