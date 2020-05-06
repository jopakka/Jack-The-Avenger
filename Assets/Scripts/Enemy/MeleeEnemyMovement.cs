using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyMovement : EnemyMovement {

    protected override void Update() {
        base.Update();
        destination = player.transform.position;
    }

    private void LateUpdate() {
        animator.SetBool("isWalking", false);
        if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon) {
            LookForward();
            animator.SetBool("isWalking", true);
        } else {
            LookPlayer();
        }
    }

    private void OnTriggerStay(Collider other) {
        // When player enters trigger zone sets playerInRange to TRUE
        // and starts to look at player
        if (other.gameObject == player) {
            playerInSight = true;
            navMeshAgent.isStopped = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        // Player exit to trigger zone
        if (other.gameObject == player) {
            playerInSight = false;
            navMeshAgent.isStopped = false;
        }
    }
}
