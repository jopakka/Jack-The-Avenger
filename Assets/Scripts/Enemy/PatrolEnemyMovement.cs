using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemyMovement : EnemyMovement {

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
        // Enemy has seen player and goes to last known location
        if (base.GetFindPlayer()) {
            base.GoToPlayersLastKnowLocation();

            // Enemy has reached players last known location and return back to normal route
            if (base.GetNavMeshAgent().remainingDistance < 0.5f) {
                base.SetFindPlayer(false);
                base.GoToOldDestination();
            }

        // If enemy partol is On and has waypoits set, then enemy follows the route
        } else if (enablePatrol && waypoints.Count != 0) {
            base.GetNavMeshAgent().isStopped = false;

            // Enemy has reached current waypoints and sets destination to next waypoint
            if (base.GetNavMeshAgent().remainingDistance <= 0.5f) {
                base.GetNavMeshAgent().SetDestination(waypoints[currentWaypointIndex].transform.position);
                NextWaypoint();
            }

        // Enemy partol is disabled
        } else {
            base.GetNavMeshAgent().isStopped = true;
        }
    }

    private void LateUpdate() {
        // Player is in enemy range and enemy start to look at player
        if (base.GetPlayerInRange()) {
            base.GetNavMeshAgent().isStopped = true;
            base.LookPlayer();

        // Enemy look forward when enemy is moving
        } else {
            base.GetNavMeshAgent().isStopped = false;
            if (base.GetNavMeshAgent().velocity.sqrMagnitude > Mathf.Epsilon) base.LookForward();
        }
    }

    private void NextWaypoint() {
        // There is travel back mode On and more than 2 waypoints
        if (travelBack && waypoints.Count > 2) {
            if (isTravelingBack) {
                // Decrements currentWaypoinIndex then if it is 0 set travel back mode Off
                if (--currentWaypointIndex == 0) isTravelingBack = false;

            } else {
                // Increments currentWaypointIndex then if it is equalt to waypoints Count
                // then sets travel back mode On and decrements index by 2
                if (++currentWaypointIndex == waypoints.Count) {
                    currentWaypointIndex -= 2;
                    isTravelingBack = true;
                }
            }
        } else {
            // Increments currentWaypointIndex then if it is equalt to waypoints Count
            // then sets index to 0
            if (++currentWaypointIndex == waypoints.Count) currentWaypointIndex = 0;
        }
    }
}
