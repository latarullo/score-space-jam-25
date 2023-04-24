using LootLocker.Requests;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private TMP_InputField playerNameText;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    private Action onCloseButtonAction;


    private void Awake() {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() => {
            this.Hide();
            onCloseButtonAction();
        });
    }

    private void Start() {
        NeedyCatGameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        LootLockerSDKManager.GetPlayerName((response) => {
            if (response.success) {
                playerNameText.text = response.name;
            } else {
                Debug.Log("Could not set player name:" + response.Error);
            }
        });

        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();
    }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;
        this.gameObject.SetActive(true);
        UpdateVisual();
        soundEffectsButton.Select();
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }
}