using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HeightScore : MonoBehaviour
{

    public Leaderboard leaderboard;
    public TextMeshProUGUI scoretext;

    public GameObject MaxHeightMarker;
    public float offset;

    public float heightScore = 0;
    public int moddedScore;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > heightScore)
        {
            heightScore = transform.position.y;
            MaxHeightMarker.transform.position = new Vector3(transform.position.x, heightScore-offset, 0f);

            moddedScore = Convert.ToInt32(heightScore * 100);
            scoretext.text =  moddedScore.ToString();
        }


    }


}
