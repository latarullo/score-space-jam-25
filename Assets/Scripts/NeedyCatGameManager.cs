using System;
using Unity.VisualScripting;
using UnityEngine;

public class NeedyCatGameManager : MonoBehaviour {

    public event EventHandler OnGameOver;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
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
    private bool gameOver = false;
    private System.Random random = new System.Random();

    private void Awake() {
        Instance = this;
        state = State.GamePlaying;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        PauseGame();
    }

    [Obsolete]
    private void Update() {
        switch (state) {
            case State.GamePlaying:
                survivalScore += Time.deltaTime;
                gameOver = random.Next(0, 100000) >= 99999;
                if (gameOver) {
                    state = State.GameOver;
                }
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

    public void PauseGame() {
        isPaused = !isPaused;
        if (isPaused) {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0;
            previousState = state;
            state = State.GamePaused;
        } else {
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1;
            state = previousState;
        }
    }
}