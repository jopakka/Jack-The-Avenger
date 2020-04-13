using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyMovement : MonoBehaviour {
    GameObject player;
    NavMeshAgent nav;
    bool playerInRange;
    float lookSpeed = 10f;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;
    }

    private void Update() {
        nav.SetDestination(player.transform.position);
    }

    private void LateUpdate() {
        if (nav.velocity.sqrMagnitude > Mathf.Epsilon) {
            Quaternion look = Quaternion.LookRotation(nav.velocity.normalized);
            look = Quaternion.Euler(new Vector3(0f, look.eulerAngles.y, 0f));
            transform.rotation = Quaternion.Slerp(transform.rotation, look, lookSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            playerInRange = false;
        }
    }
}
