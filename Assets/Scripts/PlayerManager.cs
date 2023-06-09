using UnityEngine;
using LootLocker.Requests;
using System.Collections;
using TMPro;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private Leaderboard leaderboard;
    [SerializeField] private TMP_InputField playerNameInputField;

    [System.Obsolete]
    void Start() {
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName() {
        LootLockerSDKManager.SetPlayerName(playerNameInputField.text, (response) => {
            if (response.success) {
                Debug.Log("Player name set successfully");
                PlayerPrefs.SetString("PlayerName", playerNameInputField.text);
            } else {
                Debug.Log("Could not set player name" + response.Error);
            }
        });
    }

    [System.Obsolete]
    IEnumerator SetupRoutine() {
        yield return LoginRoutine();
        //yield return leaderboard.FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine() {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) => {
            if (response.success) {
                Debug.Log("Player was logged in.");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            } else {
                Debug.Log("Could not start session.");
                done = true;
            }
        });

        yield return new WaitWhile(() => !done);
    }
}
