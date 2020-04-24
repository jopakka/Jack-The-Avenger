using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour{
    float _gizmoRadius = 3f;

    // Draw gizmos to editor window
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _gizmoRadius / 10f);
    }
}
