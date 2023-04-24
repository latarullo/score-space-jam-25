using UnityEngine;
using TMPro;
using System;

public class GameUI : MonoBehaviour {
    public static GameUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI lblScore;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        NeedyCatGameManager.Instance.OnScoreUpdate += NeedyCatGameManager_OnScoreUpdate; ;
    }

    private void NeedyCatGameManager_OnScoreUpdate(object sender, EventArgs e) {
        lblScore.text = NeedyCatGameManager.Instance.GetScore().ToString();
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }

    public void Show() {
        this.gameObject.SetActive(true);
    }
}
