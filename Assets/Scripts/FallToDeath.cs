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
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //other.gameObject.SetActive(false);

            //Destroy(other.gameObject);
            gameManager.Dead();
           // StartCoroutine(FellToDeath());

        }
    }

    //IEnumerator FellToDeath()
    //{

    //    yield return new WaitForSecondsRealtime(1f);
    //    Time.timeScale = 0f;
    //    yield return leaderboard.SubmitScoreRoutine(Convert.ToInt32(heightScore.moddedScore));
    //    Time.timeScale = 1f;
    //    gameManager.Dead();
    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    //}


}
