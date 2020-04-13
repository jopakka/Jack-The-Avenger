using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour{
    [SerializeField]
    float timeBetweenAttacks = 0.5f;
    [SerializeField]
    int attackDamage = 10;

    GameObject player;
    bool playerInRange;
    float timer;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject == player) {
            playerInRange = false;
        }
    }

    private void Update() {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange) {
            Attack();
        }
    }

    private void Attack() {
        timer = 0f;

        Debug.Log(this.name + " harms the player");
    }
}
