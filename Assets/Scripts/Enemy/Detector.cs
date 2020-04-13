using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour{
    GameObject player;
    GameObject parent;
    float angle;
    float fieldOfView;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        parent = transform.parent.gameObject;
    }

    private void Update() {
        Vector3 target = new Vector3(player.transform.position.x - parent.transform.position.x, 0f,
            player.transform.position.z - parent.transform.position.z);
        angle = Vector3.Angle(target, parent.transform.forward);
        Debug.Log("Angle: " + angle);
    }
    
    private void OnTriggerStay(Collider other) {
        if(other.gameObject == player && Mathf.Abs(angle) <= fieldOfView) {
            parent.GetComponent<SmgEnemyMovement>().SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            parent.GetComponent<SmgEnemyMovement>().SetPlayerInRange(false);
        }
    }

    public void SetFieldOfView(float angle) {
        fieldOfView = angle / 2;
    }
}
