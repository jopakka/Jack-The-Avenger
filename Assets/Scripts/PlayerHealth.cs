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
    [SerializeField]
    Weapon weapon1;
    [SerializeField]
    Weapon weapon2;
    static bool isDead;
    bool damaged;

    void Start() {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<CharacterMovement>();
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
        if(weapon1 != null) weapon1.GetComponent<Weapon>().enabled = false;
        if(weapon2 != null) weapon2.GetComponent<Weapon>().enabled = false;
        GetComponent<WeaponHandler>().enabled = false;
        GetComponent<UserInput>().enabled = false;

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
