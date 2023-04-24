using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button mainMenuButton;

    [System.Obsolete]
    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            NeedyCatGameManager.Instance.PauseGame();
        });

        mainMenuButton.onClick.AddListener(() => {
            Loader.LoadScene(Loader.Scene.MainMenuScene);
        });

        optionsButton.onClick.AddListener(() => {
            this.Hide();
            OptionsUI.Instance.Show(Show);
        });

        leaderboardButton.onClick.AddListener(() => {
            this.Hide();
            LeaderboardUI.Instance.Show(Show);
        });
    }


    private void Start() {
        NeedyCatGameManager.Instance.OnGamePaused += NeedyCatGameManager_OnGamePaused;
        NeedyCatGameManager.Instance.OnGameUnpaused += NeedyCatGameManager_OnGameUnpaused;

        this.Hide();
    }

    private void NeedyCatGameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        this.Hide();
    }
    private void NeedyCatGameManager_OnGamePaused(object sender, System.EventArgs e) {
        this.Show();
    }

    public void Show() {
        this.gameObject.SetActive(true);
        resumeButton.Select();
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }

}