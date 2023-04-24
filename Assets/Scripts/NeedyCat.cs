using System;
using UnityEngine;

public class NeedyCat : MonoBehaviour {

    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float rayDistance;
    [SerializeField] private Transform waypoints;
    private Animator animator;
    private Vector3 targetPosition;


    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        targetPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log("Distancia: " + Vector3.Distance(this.transform.position, this.targetPosition));
        if (Vector3.Distance(this.transform.position, this.targetPosition) > 1){
            Vector3 movimentar = (targetPosition - transform.position).normalized;
            this.transform.position += (new Vector3(movimentar.x, 0, movimentar.z) * Time.deltaTime);
        } else {
            Debug.Log("Criando novo alvo" + targetPosition);
            targetPosition = waypoints.position;
        }
    }
}
