using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    //public Slider healthSlider;

    Animator anim;
    CharacterMovement playerMovement;
    Weapon playerShooting;
    bool isDead;
    bool damaged;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<CharacterMovement>();
        playerShooting = GetComponentInChildren<Weapon>();
        currentHealth = startingHealth;
    }
    void Update()
    {
       
        damaged = false;
    }
    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        //healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
}
