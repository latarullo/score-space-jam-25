using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour {
    public static LeaderboardUI Instance { get; private set; }

    [SerializeField] private Button closeButton;

    [SerializeField] private Leaderboard leaderboard;

    private Action onCloseButtonAction;

    
    private void Awake() {
        Instance = this;

        closeButton.onClick.AddListener(() => {
            this.Hide();
            onCloseButtonAction();
        });
    }

    
    private void Start() {
        this.Hide();
    }

    [Obsolete]
    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;
        this.gameObject.SetActive(true);
        StartCoroutine(leaderboard.FetchTopHighscoresRoutine());
        closeButton.Select();
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }
}
