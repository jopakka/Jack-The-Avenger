using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour{
    [SerializeField]
    float timeBetweenAttacks = 0.5f;
    [SerializeField]
    int attackDamage = 10;

    GameObject player;
    //PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    //Animator anim;
    bool playerInRange;
    float timer;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        //playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        //anim = GetComponent<Animator>();
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

        if(timer >= timeBetweenAttacks && playerInRange/* && enemyHealth.currentHealth > 0*/) {
            Attack();
        }

        /*if(playerHealth.currentHealth <= 0) {
            anim.SetTrigger("PlayerDead");
        }*/
    }

    private void Attack() {
        timer = 0f;

        /*if(playerHealth > 0) {
            playerHealth.TakeDamage(attackDamage);
        }*/

        Debug.Log(this.name + " harms the player");
    }
}
