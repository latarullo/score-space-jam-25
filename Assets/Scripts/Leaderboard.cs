using UnityEngine;
using LootLocker.Requests;
using System.Collections;
using LootLocker;
using TMPro;

public class Leaderboard : MonoBehaviour {

    private int leaderboardId = 13636;
    [SerializeField] private TextMeshProUGUI lblPlayerNames;
    [SerializeField] private TextMeshProUGUI lblPlayerScores;

    void Start() {
    }

    [System.Obsolete]
    public IEnumerator SubmitScoreRoutine(int score) {
        bool done = false;
        string playerId = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerId, score, leaderboardId, (response) => {
            if (response.success) {
                Debug.Log("Successfully uploaded score");
                done = true;
            } else {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });

        yield return new WaitWhile(() => !done);
    }

    [System.Obsolete]
    public IEnumerator FetchTopHighscoresRoutine() {
        bool done = false;
        
        LootLockerSDKManager.GetScoreListMain(leaderboardId, 10, 0, (response) => {
            if (response.success) {
                Debug.Log("OK - HIGHSCORES...");

                string tempPlayerNames = "";
                string tempPlayerScores = "";

                LootLockerLeaderboardMember[] members = response.items;

                foreach (var member in members) {
                    tempPlayerNames += member.rank + "- ";
                    if (member.player.name != "") {
                        tempPlayerNames += member.player.name;
                    } else {
                        tempPlayerNames += member.player.id;
                    }
                    tempPlayerNames += "\n";
                    tempPlayerScores += member.score + "\n";

                    lblPlayerNames.text = tempPlayerNames;
                    lblPlayerScores.text = tempPlayerScores;
                }
                done = true;
            } else {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });

        yield return new WaitWhile(() => !done);
    }
}
