using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("Spawn info")]
    public GameObject SpawnPrefab;
    public float SpawnRate = 1f;
    float SpawnCountDown = 0;
    //public float meteorForce = 1;
    public Vector2 SpawnRange = Vector2.zero;

    float randPos;

    // Start is called before the first frame update
    void Start()
    {
        randPos = Random.Range(SpawnRange.x, SpawnRange.y);
        transform.position = new Vector3(randPos, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (SpawnCountDown < 0f)
        {
            spawn();
            SpawnCountDown = 1f / SpawnRate;

        }

        SpawnCountDown -= Time.deltaTime;


    }

    void spawn()
    {
        randPos = Random.Range(SpawnRange.x, SpawnRange.y);
        transform.position = new Vector3(randPos, transform.position.y, transform.position.z);

        GameObject SpawnGO = (GameObject)Instantiate(SpawnPrefab,transform.position,transform.rotation);
    }
}
