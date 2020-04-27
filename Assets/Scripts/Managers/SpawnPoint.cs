using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour{
    float _gizmoRadius = 3f;

    // Draw gizmos to editor window
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _gizmoRadius / 10f);
    }
}
