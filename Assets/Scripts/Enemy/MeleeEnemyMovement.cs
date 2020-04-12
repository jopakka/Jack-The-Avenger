using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyMovement : MonoBehaviour {
    GameObject player;
    NavMeshAgent nav;
    //EnemyHealth enemyHealth;
    //PlayerHealth playerHealth;
    bool playerInRange;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        //enemyhealth = GetComponent<EnemyHealth>();
        //playerHealth = GetComponent<playerHealth>();
    }

    private void Update() {
        //if(enemyHealth > 0 && playerHealth > 0) {
        if (playerInRange) {
            nav.isStopped = true;
        } else {
            nav.isStopped = false;
            nav.SetDestination(player.transform.position);
        } //else
        //  nav.enabled = false;
        //}
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
