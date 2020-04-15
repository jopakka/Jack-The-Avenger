using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyMovement : EnemyMovement {

    private void Update() {
        base.SetDestination(base.GetPlayer().transform.position);
    }

    private void LateUpdate() {
        if (base.GetNavMeshAgent().velocity.sqrMagnitude > Mathf.Epsilon) {
            base.LookForward();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject == base.GetPlayer()) {
            base.SetPlayerInRange(true);
            base.GetNavMeshAgent().isStopped = true;
            base.LookPlayer();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == base.GetPlayer()) {
            base.SetPlayerInRange(false);
            base.GetNavMeshAgent().isStopped = false;
        }
    }
}
