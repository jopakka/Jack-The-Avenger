using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmgEnemyMovement : EnemyMovement {

    #region Detector variables

    [Header("Player Detector")]
    [SerializeField]
    float detectorRange = 5f;
    [SerializeField]
    [Range(0f, 360f)]
    float fieldOfView = 40f;
    GameObject detector;

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

    protected override void Start() {
        base.Start();
        detector = transform.Find("Detector").gameObject;
        detector.GetComponent<SphereCollider>().radius = detectorRange;
        detector.GetComponent<EnemyDetector>().SetFieldOfView(fieldOfView);
    }

    private void Update() {
        if (base.GetFindPlayer()) {
            base.GoToPlayersLastKnowLocation();
            if(base.GetNavMeshAgent().remainingDistance < 0.5f) {
                base.SetFindPlayer(false);
                base.GoToOldDestination();
            }
        } else if (enablePatrol && waypoints.Count != 0) {
            base.GetNavMeshAgent().isStopped = false;
            if (base.GetNavMeshAgent().remainingDistance <= 0.5f) {
                base.GetNavMeshAgent().SetDestination(waypoints[currentWaypointIndex].transform.position);
                NextWaypoint();
            }
        } else {
            base.GetNavMeshAgent().isStopped = true;
        }
    }

    private void LateUpdate() {
        if (base.GetPlayerInRange()) {
            base.GetNavMeshAgent().isStopped = true;
            base.LookPlayer();
        } else {
            base.GetNavMeshAgent().isStopped = false;
            if (base.GetNavMeshAgent().velocity.sqrMagnitude > Mathf.Epsilon) base.LookForward();
        }
    }

    private void NextWaypoint() {
        if (travelBack && waypoints.Count > 2) {
            if (isTravelingBack) {
                if (--currentWaypointIndex == 0) isTravelingBack = false;
            } else {
                if (++currentWaypointIndex == waypoints.Count) {
                    currentWaypointIndex -= 2;
                    isTravelingBack = true;
                }
            }
        } else {
            if (++currentWaypointIndex == waypoints.Count) currentWaypointIndex = 0;
        }
    }
}
