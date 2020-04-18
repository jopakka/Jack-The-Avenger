using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemyMovement : EnemyMovement {

    #region Detector variables

    [Header("Player Detector")]
    [SerializeField]
    float _detectorRange = 5f;
    [SerializeField]
    [Range(0f, 360f)]
    float _fieldOfView = 40f;
    GameObject _detector;

    #endregion

    #region Patrol variables

    [Header("Patrol")]
    [SerializeField]
    bool _enablePatrol;
    [SerializeField]
    bool _travelBack;
    [SerializeField]
    List<Waypoint> _waypoints;
    int _currentWaypointIndex;
    bool _isTravelingBack;

    #endregion

    protected override void Start() {
        base.Start();
        _detector = transform.Find("Detector").gameObject;
        _detector.GetComponent<SphereCollider>().radius = _detectorRange;
        _detector.GetComponent<EnemyDetector>().fieldOfView = _fieldOfView;
    }

    private void Update() {
        // Enemy has seen player and goes to last known location
        if (base.findPlayer) {
            base.GoToPlayersLastKnowLocation();

            // Enemy has reached players last known location and return back to normal route
            if (base.navMeshAgent.remainingDistance < 0.5f) {
                base.findPlayer = false;
                base.GoToOldDestination();
            }

        // If enemy partol is On and has waypoits set, then enemy follows the route
        } else if (_enablePatrol && _waypoints.Count != 0) {
            base.navMeshAgent.isStopped = false;

            // Enemy has reached current waypoints and sets destination to next waypoint
            if (base.navMeshAgent.remainingDistance <= 0.5f) {
                base.navMeshAgent.SetDestination(_waypoints[_currentWaypointIndex].transform.position);
                NextWaypoint();
            }

        // Enemy partol is disabled
        } else {
            base.navMeshAgent.isStopped = true;
        }
    }

    private void LateUpdate() {
        // Player is in enemy range and enemy start to look at player
        if (base.playerInRange) {
            base.navMeshAgent.isStopped = true;
            base.LookPlayer();

        // Enemy look forward when enemy is moving
        } else {
            base.navMeshAgent.isStopped = false;
            if (base.navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon) base.LookForward();
        }
    }

    private void NextWaypoint() {
        // There is travel back mode On and more than 2 waypoints
        if (_travelBack && _waypoints.Count > 2) {
            if (_isTravelingBack) {
                // Decrements currentWaypoinIndex then if it is 0 set travel back mode Off
                if (--_currentWaypointIndex == 0) _isTravelingBack = false;

            } else {
                // Increments currentWaypointIndex then if it is equalt to waypoints Count
                // then sets travel back mode On and decrements index by 2
                if (++_currentWaypointIndex == _waypoints.Count) {
                    _currentWaypointIndex -= 2;
                    _isTravelingBack = true;
                }
            }
        } else {
            // Increments currentWaypointIndex then if it is equalt to waypoints Count
            // then sets index to 0
            if (++_currentWaypointIndex == _waypoints.Count) _currentWaypointIndex = 0;
        }
    }
}
