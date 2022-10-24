using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using UnityEngine.UIElements;
using System;

public class Leaderboard : MonoBehaviour
{
    int leaderboardID = 8163;
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;

    public bool canUploadScore;

    public Transform MaxHeightPos;

    public GameObject JumpRest;

    public int maxScores = 10;
    public int minScores = 0;

    public GameObject Holder;

    public bool PlayMode;


    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID") + GetAndIncrementScoreCharacters();
        string metadata = PlayerPrefs.GetString("PlayerName") + "-!" + (Mathf.Round(MaxHeightPos.position.x)).ToString() + "@" + (Mathf.Round(MaxHeightPos.position.y)).ToString();

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
        LootLockerSDKManager.GetScoreList(leaderboardID, maxScores, minScores, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    string CleanName = members[i].metadata.Substring(0, members[i].metadata.IndexOf("-"));
                    string tempString = members[i].metadata;
                    int from = tempString.IndexOf("!") + 1;
                    int to = tempString.IndexOf("@");
                    string Xresult = tempString.Substring(from, to - from);
                    to++;
                    string Yresult = tempString.Substring(to, tempString.Length - to );

                    float xPos = float.Parse(Xresult);
                    float yPos = float.Parse(Yresult);

                    tempPlayerNames += members[i].rank + ". " + CleanName + "\n";

                    tempPlayerScores += members[i].score + "\n";


                    if (PlayMode)
                    {
                        GameObject temp = Instantiate(JumpRest, new Vector3(xPos, yPos, 0f), Quaternion.Euler(0, 0, 0));
                        temp.GetComponent<JumpRest>().fallenName = CleanName;

                        temp.transform.SetParent(Holder.transform);
                    }
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


    public IEnumerator FetchHighscoresCentered()
    {
        bool done = false;
        // Let the player know that the scores are loading
        string playerNamesTemp = "Loading...";
        string playerScoresTemp = "";

        playerScores.text = playerScoresTemp;
        playerNames.text = playerNamesTemp;

        // Get the player ID from Player prefs with the incremental score string attached
        string latestPlayerID = PlayerPrefs.GetString("PlayerID") + PlayerPrefs.GetString("incrementalScoreString");
        string[] memberIDs = new string[1] { latestPlayerID };

        // Get the score that matches this ID
        LootLockerSDKManager.GetByListOfMembers(memberIDs, leaderboardID, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");

                // We're only asking for one player, so we just need to check the first entry
                int rank = response.members[0].rank;
                int count = 10;

                // 5 before and 5 after
                int after = rank < 6 ? 0 : rank - 5;

                // Get the entries based on the rank that we just found
                LootLockerSDKManager.GetScoreList(leaderboardID, count, after, (response) =>
                {
                    if (response.statusCode == 200)
                    {
                        // Set the title of the names tab
                        playerNames.text = "Names\n";
                        // Set the title of the scores tab
                        playerScores.text = "Score\n";

                        Debug.Log("Successful");

                        LootLockerLeaderboardMember[] members = response.items;
                        for (int i = 0; i < members.Length; i++)
                        {
                            string CleanName = members[i].metadata.Substring(0, members[i].metadata.IndexOf("-"));
                            // Highlight the new score with yellow, add the rest as normal

                            if (members[i].rank == rank)
                            {
                                playerNamesTemp += "<color=#f4e063ff>" + members[i].rank + ". " + CleanName + "\n";
                                playerScoresTemp += "<color=#f4e063ff>" + members[i].score + "\n";
                            }
                            else
                            {
                                playerNamesTemp += members[i].rank + ". " + CleanName + "\n";
                                playerScoresTemp += members[i].score + "\n";
                            }

                        }
                        done = true;
                        playerNames.text = playerNamesTemp;
                        playerScores.text = playerScoresTemp;
                    }
                    else
                    {
                        Debug.Log("failed: " + response.Error);
                    }
                });
            }
            else
            {
                Debug.Log("failed: " + response.Error);
            }
        });

        // Wait until request is done
        yield return new WaitWhile(() => done == false);

        // Update the TextMeshPro components

    }
}