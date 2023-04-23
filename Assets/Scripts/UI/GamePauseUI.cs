using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            //NeedyCatGameManager.Instance.PauseGame();
        });

        mainMenuButton.onClick.AddListener(() => {
            Loader.LoadScene(Loader.Scene.MainMenuScene);
        });

        optionsButton.onClick.AddListener(() => {
            this.Hide();
            //OptionsUI.Instance.Show(Show);
        });
    }


    private void Start() {
        //NeedyCatGameManager.Instance.OnGamePaused += NeedyCatManager_OnGamePaused;
        //NeedyCatGameManager.Instance.OnGameUnpaused += NeedyCatManager_OnGameUnpaused;

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