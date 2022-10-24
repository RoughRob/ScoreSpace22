using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float length, startPos;
    private GameObject cam;
    public float ParallaxMulti = 0;

    public void Awake()
    {
        cam = GameObject.Find("Main Camera");
    }


    private void Start()
    {
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;

    }

    // Update is called once per frame
    void FixedUpdate()
    {


    }

    private void Update()
    {
        float dist = (cam.transform.position.y * ParallaxMulti);

        transform.position = new Vector3(transform.position.x, startPos + dist, transform.position.z);

    }
}