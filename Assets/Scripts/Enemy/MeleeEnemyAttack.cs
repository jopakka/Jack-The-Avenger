using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour{
    [SerializeField]
    float timeBetweenAttacks = 0.5f;
    [SerializeField]
    int attackDamage = 10;

    float timer;
    MeleeEnemyMovement movement;
    GameObject player;

    private void Awake() {
        movement = GetComponent<MeleeEnemyMovement>();
        player = movement.GetPlayer();
    }

    private void Update() {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && movement.GetPlayerInRange()) {
            Attack();
        }
    }

    private void Attack() {
        timer = 0f;

        Debug.Log(this.name + " harms the player");
    }
}
