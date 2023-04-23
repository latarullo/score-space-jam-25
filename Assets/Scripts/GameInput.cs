using System;
using UnityEngine;

public class GameInput : MonoBehaviour {

    [SerializeField] private Transform needyCat;
    [SerializeField] private Transform hooman;
    [SerializeField] private Leaderboard leaderboard;

    void Start() {
    }

    [Obsolete]
    void Update() {
        Vector3 needyCatForwardVector = needyCat.transform.forward;
        Vector3 hoomanForwardVector = hooman.transform.forward;

        //Debug.Log(Vector3.Dot(needyCatForwardVector, hoomanForwardVector));

        if (Input.GetKeyDown(KeyCode.Z)) {
            StartCoroutine(leaderboard.SubmitScoreRoutine(1000));
        }
    }
}
