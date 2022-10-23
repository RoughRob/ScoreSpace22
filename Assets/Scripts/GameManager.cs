using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public GameObject PlayUI;
    public GameObject DeadUI;
    public GameObject PauseUI;   

    public Leaderboard Leaderboard;
    public HeightScore HeightScore;

    public TextMeshProUGUI FinalScore;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (PauseUI.activeSelf == false)
            {
                Pause();
            }
            else if (PauseUI.activeSelf == true)
            {
                Play();
            }
        }

    }

    public void Dead()
    {
        //the player has died
        Time.timeScale = 0f;
        PlayUI.SetActive(false);
        DeadUI.SetActive(true);

        FinalScore.text = HeightScore.scoretext.text;


        StartCoroutine(FellToDeath());

    }
    public void Menu()
    {
        // Entered Menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void Play()
    {
        //Play the game
        Time.timeScale = 1f;
        PauseUI.SetActive(false);
       // PlayUI.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
       // PlayUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }


    IEnumerator FellToDeath()
    {

        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0f;
        yield return Leaderboard.SubmitScoreRoutine(Convert.ToInt32(HeightScore.moddedScore));

        Leaderboard.PlayMode = false;
        yield return Leaderboard.FetchTopHighscoresRoutine();
        //yield return Leaderboard.FetchHighscoresCentered();
        Leaderboard.PlayMode = true;

        Time.timeScale = 1f;
        //gameManager.Dead();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
