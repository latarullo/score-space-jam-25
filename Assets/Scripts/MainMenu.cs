using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    private void Start() {

        StartCoroutine(hideMenuIEnumerator());
    }

    private IEnumerator hideMenuIEnumerator() {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
