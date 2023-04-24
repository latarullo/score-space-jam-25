using System;
using Unity.VisualScripting;
using UnityEngine;

public class NeedyCatGameManager : MonoBehaviour {

    public event EventHandler OnGameOver;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public event EventHandler OnScoreUpdate;

    public static NeedyCatGameManager Instance { get; private set; }

    private enum State {
        GamePlaying,
        GamePaused,
        GameOver,
    }
    private State previousState;
    private State state;
    private bool isPaused = false;
    private float survivalScore = 0;

    private void Awake() {
        Instance = this;
        state = State.GamePlaying;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        NeedyCat.Instance.OnHoomanSpotted += NeedyCat_OnHoomanSpotted;
    }

    private void NeedyCat_OnHoomanSpotted(object sender, EventArgs e) {
        GameUI.Instance.Hide();
        this.state = State.GameOver;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        PauseGame();
    }

    [Obsolete]
    private void Update() {
        switch (state) {
            case State.GamePlaying:
                survivalScore += Time.deltaTime;
                OnScoreUpdate?.Invoke(this, EventArgs.Empty);
                break;
            case State.GamePaused:
                break;
            case State.GameOver:
                GameOverUI.Instance.Show();
                Time.timeScale = 0;
                previousState = state;
                state = State.GamePaused;
                break;
        }
        Debug.Log("State : " + state);
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public int GetScore() {
        return Convert.ToInt32(survivalScore);
    }

    public float GetSurvivalScore() {
        return survivalScore;
    }

    public void PauseGame() {
        isPaused = !isPaused;
        if (isPaused) {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0;
            previousState = state;
            state = State.GamePaused;
            GameUI.Instance.Hide();
        } else {
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1;
            state = previousState;
            GameUI.Instance.Show();
        }
    }
}