using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public void Scores()
    {
        SceneManager.LoadScene("Scores");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
