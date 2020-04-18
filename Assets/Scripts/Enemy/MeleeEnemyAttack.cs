using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour{

    #region Attack variables

    [SerializeField]
    float _timeBetweenAttacks = 0.5f;
    [SerializeField]
    int _attackDamage = 10;
    [SerializeField]
    [Range(0f, 100f)]
    float _hitChange = 90f;

    #endregion

    #region Misc variables

    float timer;
    MeleeEnemyMovement movement;
    GameObject player;

    #endregion

    private void Awake() {
        movement = GetComponent<MeleeEnemyMovement>();
        player = movement.player;
    }

    private void Update() {
        timer += Time.deltaTime;

        // When timer is greater than timeBetweenAttacks and player is in range
        // attack player
        if (timer >= _timeBetweenAttacks && movement.playerInRange) {
            Attack();
        }
    }

    private void Attack() {
        timer = 0f;

        Debug.Log(this.name + " tries to hit player");

        if (Random.Range(0f, 100f) <= _hitChange) DamagePlayer();
    }

    private void DamagePlayer() {
        Debug.Log(this.name + " harms the player");
    }
}
