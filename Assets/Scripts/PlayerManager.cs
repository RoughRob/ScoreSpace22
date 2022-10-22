using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerManager : MonoBehaviour
{

    public bool loggedIn;
    public Leaderboard leaderboard;
    public TMP_InputField playerNameInputfield;

    public int incrementalIDInt;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupRoutine());
    }


    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine()
    {
        incrementalIDInt = PlayerPrefs.GetInt(nameof(incrementalIDInt));
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully started LootLocker session");
                loggedIn = true;
                done = true;
                // Save the player ID for use in the leaderboard
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                // If the players name hasn't been set, set it to #GuestXXXXXXX (XXXXX = player ID)
                PlayerPrefs.SetString("PlayerName", PlayerPrefs.GetString("PlayerName", "#Guest" + PlayerPrefs.GetString("PlayerID")));
            }
            else
            {
                Debug.Log("Error starting LootLocker session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);


        UpdatePlayerInputField();

    }



    //public void SetPlayerName()
    //{
    //    LootLockerSDKManager.SetPlayerName(playerNameInputfield.text, (response) =>
    //    {
    //        if (response.success)
    //        {
    //            Debug.Log("Succesfully set player name");
    //        }
    //        else
    //        {
    //            Debug.Log("Could not set player name" + response.Error);
    //        }
    //    });

    //    //PlayerPrefs.SetString("PlayerName", playerNameInputfield.text);
    //}


    public void UpdatePlayerName()
    {
        PlayerPrefs.SetString("PlayerName", playerNameInputfield.text);
    }

    public void UpdatePlayerInputField()
    {
        // Set the text to the player name
        playerNameInputfield.text = PlayerPrefs.GetString("PlayerName");

        // Set the placeholder name to the player name as well
        TextMeshProUGUI placeHolderText = playerNameInputfield.placeholder as TextMeshProUGUI;
        placeHolderText.text = PlayerPrefs.GetString("PlayerName");
    }

}