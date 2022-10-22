using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreTest : MonoBehaviour
{

    public Leaderboard leaderboard;
    public TextMeshProUGUI scoretext;

    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
                    score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            score++;
            scoretext.text = score.ToString();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(EndTest());
        }
    }

    IEnumerator EndTest()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        yield return leaderboard.SubmitScoreRoutine(score);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
