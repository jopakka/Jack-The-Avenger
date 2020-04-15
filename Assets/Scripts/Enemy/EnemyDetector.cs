using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour{
    GameObject player;
    GameObject parent;
    float fieldOfView;
    EnemyMovement movement;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        parent = transform.parent.gameObject;
        movement = parent.GetComponent<EnemyMovement>();
    }
    
    private void OnTriggerStay(Collider other) {
        Vector3 target = new Vector3(player.transform.position.x - parent.transform.position.x, 0f,
            player.transform.position.z - parent.transform.position.z);
        float angle = Vector3.Angle(target, parent.transform.forward);

        if (other.gameObject == player && Mathf.Abs(angle) <= fieldOfView) {
            movement.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            movement.SetPlayerInRange(false);
        }
    }

    public void SetFieldOfView(float angle) {
        fieldOfView = angle / 2f;
    }
}
