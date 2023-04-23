using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsUI : MonoBehaviour
{
    private GameObject caller;

    public void Show(GameObject caller) {
        this.caller = caller;
        this.gameObject.SetActive(true);
    }
    public void Hide() {
        this.caller?.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
