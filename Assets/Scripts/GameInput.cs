using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set; }

    public event EventHandler OnPauseAction;
    public event EventHandler OnGameOverAction;

    private void Awake() {
        Instance = this;
    }

    [SerializeField] private Transform needyCat;
    [SerializeField] private Transform hooman;

    void Start() {
    }

    [Obsolete]
    void Update() {
        Vector3 needyCatForwardVector = needyCat.transform.forward;
        Vector3 hoomanForwardVector = hooman.transform.forward;

        //Debug.Log(Vector3.Dot(needyCatForwardVector, hoomanForwardVector));

        if (Input.GetKeyDown(KeyCode.P)) {
            Debug.Log("P - pressed - Pausing/Unpausing...");
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            Debug.Log("G - pressed - GameOver...");
            OnGameOverAction?.Invoke(this, EventArgs.Empty);
        }
    }
}
