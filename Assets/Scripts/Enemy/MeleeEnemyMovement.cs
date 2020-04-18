using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyMovement : EnemyMovement {

    private void Update() {
        base.destination = base.player.transform.position;
    }

    private void LateUpdate() {
        if (base.navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon) {
            base.LookForward();
        }
    }

    private void OnTriggerStay(Collider other) {
        // When player enters trigger zone sets playerInRange to TRUE
        // and starts to look at player
        if (other.gameObject == base.player) {
            base.playerInRange = true;
            base.navMeshAgent.isStopped = true;
            base.LookPlayer();
        }
    }

    private void OnTriggerExit(Collider other) {
        // Player exit to trigger zone
        if (other.gameObject == base.player) {
            base.playerInRange = false;
            base.navMeshAgent.isStopped = false;
        }
    }
}
