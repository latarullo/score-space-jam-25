using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    public static GameOverUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button retryButton;

    [SerializeField] private Leaderboard leaderboard;

    [System.Obsolete]
    private void Awake() {
            Instance = this;
            leaderboardButton.onClick.AddListener(() => {
            this.Hide();
            LeaderboardUI.Instance.Show(Show);
        });

        retryButton.onClick.AddListener(() => {
            Time.timeScale = 1;
            Loader.LoadScene(Loader.Scene.GameScene);
        });
    }

    [System.Obsolete]
    private void Start() {
        Hide();
    }

    [System.Obsolete]
    public void Show() {
        this.gameObject.SetActive(true);

        int score = NeedyCatGameManager.Instance.GetScore();
        scoreText.text = score.ToString();
        StartCoroutine(leaderboard.SubmitScoreRoutine(score));

        retryButton.Select();
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }
}
