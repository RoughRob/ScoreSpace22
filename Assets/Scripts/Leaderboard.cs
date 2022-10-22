using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    int leaderboardID = 7999;
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;

    public bool canUploadScore;
    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID") + GetAndIncrementScoreCharacters();
        string metadata = PlayerPrefs.GetString("PlayerName");

        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID.ToString(), metadata, (response) =>
        {
            if (response.statusCode == 200)
            {
                // Only let the player upload score once until we reset it
                canUploadScore = false;
                Debug.Log("Successful");
                done = true;
            }
            else
            {
                Debug.Log("failed: " + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    // Increment and save a string that goes from a to z, then za to zz, zza to zzz etc.
    string GetAndIncrementScoreCharacters()
    {
        // Get the current score string
        string incrementalScoreString = PlayerPrefs.GetString(nameof(incrementalScoreString), "a");

        // Get the current character
        char incrementalCharacter = PlayerPrefs.GetString(nameof(incrementalCharacter), "a")[0];

        // If the previous character we added was 'z', add one more character to the string.
        // Otherwise, replace last character of the string with the current incrementalCharacter
        if (incrementalScoreString[incrementalScoreString.Length - 1] == 'z')
        {
            // Add one more character
            incrementalScoreString += incrementalCharacter;
        }
        else
        {
            // Replace character
            incrementalScoreString = incrementalScoreString.Substring(0, incrementalScoreString.Length - 1) + incrementalCharacter.ToString();
        }

        // If the letter int is lower than 'z' add to it otherwise start from 'a' again
        if ((int)incrementalCharacter < 122)
        {
            incrementalCharacter++;
        }
        else
        {
            incrementalCharacter = 'a';
        }

        // Save the current incremental values to PlayerPrefs
        PlayerPrefs.SetString(nameof(incrementalCharacter), incrementalCharacter.ToString());
        PlayerPrefs.SetString(nameof(incrementalScoreString), incrementalScoreString.ToString());

        // Return the updated string
        return incrementalScoreString;
    }

    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, 10, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". " + members[i].metadata + "\n";
                    //if (members[i].player.name != "")
                    //{
                    //    tempPlayerNames += members[i].player.name;
                    //}
                    //else
                    //{
                    //    tempPlayerNames += members[i].player.id;
                    //}
                    tempPlayerScores += members[i].score + "\n";
                    //tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}