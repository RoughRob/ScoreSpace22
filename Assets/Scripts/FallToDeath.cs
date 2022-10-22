using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class FallToDeath : MonoBehaviour
{

    public HeightScore heightScore;
    public int deatheight;

    public Leaderboard leaderboard;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && heightScore.heightScore > deatheight )
        {
            //other.gameObject.SetActive(false);
            StartCoroutine(FellToDeath());
        }
    }

    IEnumerator FellToDeath()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        yield return leaderboard.SubmitScoreRoutine(Convert.ToInt32(heightScore.moddedScore));
        Time.timeScale = 1f; 
        Debug.Log("y no 2");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


}
