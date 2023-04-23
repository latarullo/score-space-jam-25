using System;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour {

    private GameObject caller;

    public void Show(GameObject caller) {
        this.caller = caller;
        this.gameObject.SetActive(true);
    }
    public void Hide() {
        this.caller.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
