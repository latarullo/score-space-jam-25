using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class NeedyCat : MonoBehaviour {

    public static NeedyCat Instance { get; private set; }

    public event EventHandler OnHoomanSpotted;

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float turnSpeed = 200;
    [SerializeField] private float rayDistance;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float minimumDistanceToReachDestination = 0.2f;
    [SerializeField] private float sittingDuration = 2;
    [SerializeField] private float fromSitToWalkDuration = 1;
    [SerializeField] private float IncreaseDifficultyTime = 10;
    [SerializeField] private float minimumScoreToRandomizeWaypoints = 50;
    [SerializeField] private float minimumScoreToChasePlayer = 100;
    [SerializeField] private float viewDistance = 1;
    [SerializeField] private float viewAngle = 60;
    [SerializeField] private LayerMask viewMask;

    [SerializeField] private GameObject hooman;

    private Animator animator;
    private Vector3 targetPosition;
    private int waypointIndex = 0;
    private bool useRandomWaypoints = false;
    private bool chasePlayer = false;

    private enum State {
        Walk,
        Sit,
    }

    private State state;
    private bool isWalking;
    private bool isSitting;
    private Quaternion lookRotation;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start() {
        Instance = this;
        animator = GetComponent<Animator>();
        state = State.Walk;
        targetPosition = waypoints[waypointIndex].position;
        isWalking = false;

        StartCoroutine(IncreaseDifficulty());
    }

    // Update is called once per frame
    void Update() {
        Turn();
        switch (state) {
            case State.Walk:
                if (!isWalking) {
                    animator.SetTrigger("Walk");
                    isWalking = true;
                }

                if (Vector3.Distance(this.transform.position, this.targetPosition) > minimumDistanceToReachDestination) {
                    Vector3 movimentar = (targetPosition - transform.position).normalized;
                    this.transform.position += (new Vector3(movimentar.x, 0, movimentar.z) * moveSpeed * Time.deltaTime);
                } else {
                    state = State.Sit;

                    Debug.Log("Criando novo alvo" + targetPosition);
                    if (chasePlayer) {
                        targetPosition = hooman.transform.position;
                    } else {
                        if (useRandomWaypoints) {
                            waypointIndex = UnityEngine.Random.Range(0, waypoints.Length);
                        } else {
                            waypointIndex = (waypointIndex + 1) % waypoints.Length;
                        }
                        targetPosition = waypoints[waypointIndex].position;
                    }

                }
                break;
            case State.Sit:
                if (isWalking) {
                    animator.ResetTrigger("Walk");
                    isWalking = false;
                }
                if (!isSitting) {
                    animator.SetTrigger("Sit");
                    isSitting = true;
                    StartCoroutine(SitAndBackToNextWalk());
                }

                break;
        }
    }

    private IEnumerator SitAndBackToNextWalk() {
        bool done = false;

        yield return new WaitForSeconds(this.sittingDuration);
        if (isSitting) {
            animator.ResetTrigger("Sit");
            isSitting = false;
            this.state = State.Walk;
        }
        yield return Meow();

        done = true;
        yield return new WaitWhile(() => !done);
    }

    IEnumerator Meow() {
        SoundManager.Instance.PlayMeowSound(transform);
        Debug.Log(Vector3.Distance(this.transform.position, hooman.transform.position));

        if (Vector3.Distance(this.transform.position, hooman.transform.position) < viewDistance) {
            Vector3 dirToPlayer = (hooman.transform.position - this.transform.position).normalized;
            float angleBetween = Vector3.Angle(this.transform.forward, dirToPlayer);
            if (angleBetween < viewAngle / 2f) {
                Vector3 catPos = new Vector3(this.transform.position.x, 0.01f, this.transform.position.z);
                Vector3 hoomanPos = new Vector3(hooman.transform.position.x, 0.01f, hooman.transform.position.z);
                if (!Physics.Linecast(this.transform.position, hooman.transform.position, viewMask)){
                    OnHoomanSpotted(this, EventArgs.Empty);
                }
            }
        }
        //if (Input.GetKey(KeyCode.Q)) {
        //    OnHoomanSpotted(this, EventArgs.Empty);
        //}
        yield return new WaitForSeconds(this.fromSitToWalkDuration);
    }

    private void Turn() {
        direction = (targetPosition - transform.position).normalized;
        direction = new Vector3(direction.x, 0, direction.z);

        //create the rotation we need to be in to look at the target
        lookRotation = Quaternion.LookRotation(direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    IEnumerator IncreaseDifficulty() {
        while (true) {
            float survivalScore = NeedyCatGameManager.Instance.GetSurvivalScore();

            if (this.sittingDuration > 0.5) {
                this.sittingDuration -= 0.1f;
            }
            if (this.fromSitToWalkDuration > 0.2) {
                this.fromSitToWalkDuration -= 0.1f;
            }
            if (this.moveSpeed < 5) {
                this.moveSpeed += 0.1f;
            }

            if (survivalScore >= minimumScoreToRandomizeWaypoints && !useRandomWaypoints) {
                useRandomWaypoints = true;
            }

            if (survivalScore >= minimumScoreToChasePlayer && !chasePlayer) {
                chasePlayer = true;
            }
            
            if ((useRandomWaypoints || chasePlayer ) && viewDistance < 3) {
                viewDistance += 0.1f;
            }


            yield return new WaitForSeconds(IncreaseDifficultyTime);
        }
    }
}
