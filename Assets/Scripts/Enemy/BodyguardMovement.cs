using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyguardMovement : EnemyMovement {

    #region Detector variables

    [Header("Player Detector")]
    [SerializeField]
    float _detectorRange = 5f;
    [SerializeField]
    [Range(0f, 360f)]
    float _fieldOfView = 40f;
    GameObject _detector;

    #endregion

    #region Misc Variables

    Vector3 _guardPos;
    Quaternion _guardRot;

    #endregion

    protected override void Start() {
        base.Start();
        _guardPos = transform.position;
        _guardRot = transform.rotation;
        _detector = transform.Find("Detector").gameObject;
        _detector.GetComponent<SphereCollider>().radius = _detectorRange;
        _detector.GetComponent<EnemyDetector>().fieldOfView = _fieldOfView;
    }

    private void Update() {
        SetWalkState();

        // Player is in enemy range and enemy start to look at player
        if (playerInSight) {
            navMeshAgent.isStopped = true;
            LookPlayer();

        } else if (findPlayer && !playerInSight) {
            GoToPlayersLastKnowLocation();
            navMeshAgent.isStopped = false;

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f) {
                findPlayer = false;
                destination = _guardPos;
            }

        } else {
            if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon) {
                LookForward();
            } else {
                LookTo(_guardRot);
            }
        }
    }

    private void OnGUI() {
        string text = "*Bodyguard*";
        text += "\nremDis: " + navMeshAgent.remainingDistance;
        text += "\nplayerInSight: " + playerInSight;
        text += "\nfindPlayer: " + findPlayer;
        GUI.Box(new Rect(10, 10, 150, 100), text);
    }

    private void SetWalkState() {
        if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon) {
            animator.SetBool("IsWalking", true);
        } else {
            animator.SetBool("IsWalking", false);
        }
    }
}
