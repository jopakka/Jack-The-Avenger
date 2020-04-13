using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmgEnemyMovement : MonoBehaviour {

    #region Detector variables

    [SerializeField]
    float detectorRange = 5f;
    [SerializeField]
    [Range(0f, 360f)]
    float fieldOfView = 40f;

    #endregion

    #region Patrol variables

    [Header("Patrol")]
    [SerializeField]
    bool enablePatrol;
    [SerializeField]
    bool travelBack;
    [SerializeField]
    List<Waypoint> waypoints;
    int currentWaypointIndex;
    bool isTravelingBack;

    #endregion

    NavMeshAgent nav;
    GameObject player;
    GameObject detector;
    float lookSpeed = 10f;
    bool playerInRange;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;
        detector = transform.Find("Detector").gameObject;
        detector.GetComponent<SphereCollider>().radius = detectorRange;
        detector.GetComponent<Detector>().SetFieldOfView(fieldOfView);
    }

    private void Update() {
        if (enablePatrol && waypoints.Count != 0) {
            nav.isStopped = false;
            if (nav.remainingDistance <= 0.5f) {
                nav.SetDestination(waypoints[currentWaypointIndex].transform.position);

                NextWaypoint();
            }
        } else {
            nav.isStopped = true;
        }
    }

    private void LateUpdate() {
        if (playerInRange) {
            nav.isStopped = true;
            Quaternion look = Quaternion.LookRotation(player.transform.position - transform.position);
            look = Quaternion.Euler(new Vector3(0f, look.eulerAngles.y, 0f));
            transform.rotation = Quaternion.Slerp(transform.rotation, look, lookSpeed * Time.deltaTime);
        } else {
            nav.isStopped = false;
            if (nav.velocity.sqrMagnitude > Mathf.Epsilon) {
                Quaternion look = Quaternion.LookRotation(nav.velocity.normalized);
                look = Quaternion.Euler(new Vector3(0f, look.eulerAngles.y, 0f));
                transform.rotation = Quaternion.Slerp(transform.rotation, look, lookSpeed * Time.deltaTime);
            }
        }
    }

    private void NextWaypoint() {
        if (travelBack) {
            if (isTravelingBack) {
                if (--currentWaypointIndex == 0) {
                    isTravelingBack = false;
                }
            } else {
                if (++currentWaypointIndex == waypoints.Count) {
                    currentWaypointIndex -= 2;
                    isTravelingBack = true;
                }
            }
        } else {
            if (++currentWaypointIndex == waypoints.Count) {
                currentWaypointIndex = 0;
            }
        }
    }

    public void SetPlayerInRange(bool value) {
        playerInRange = value;
    }
}
