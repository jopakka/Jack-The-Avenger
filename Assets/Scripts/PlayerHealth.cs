using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    Animator anim;
    CharacterMovement playerMovement;
    //Weapon playerShooting;
    static bool isDead;
    bool damaged;

    void Start() {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<CharacterMovement>();
        //playerShooting = GetComponentInChildren<Weapon>();
        currentHealth = startingHealth;
    }
    void Update() {

        damaged = false;
    }
    public void TakeDamage(int amount) {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead) {
            Death();
        }
    }

    void Death() {
        anim.SetTrigger("Die");

        playerMovement.enabled = false;
        //playerShooting.enabled = false;

        isDead = true;
    }

    public void StartSinking() {
        // Do not remove
        // This prevents errors xD
    }

    public static bool IsDead {
        get => isDead;
        set => isDead = value;
    }
}
