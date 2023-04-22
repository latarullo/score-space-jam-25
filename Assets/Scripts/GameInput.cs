using UnityEngine;

public class GameInput : MonoBehaviour {

    [SerializeField] private Transform needyCat;
    [SerializeField] private Transform hooman;

    void Start() {

    }

    void Update() {
        Vector3 needyCatForwardVector = needyCat.transform.forward;
        Vector3 hoomanForwardVector = hooman.transform.forward;

        //Debug.Log(Vector3.Dot(needyCatForwardVector, hoomanForwardVector));

    }
}
