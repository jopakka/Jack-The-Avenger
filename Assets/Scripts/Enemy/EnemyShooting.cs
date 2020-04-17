using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyShooting : MonoBehaviour {

    #region Gun variables

    [SerializeField]
    int clipSize = 30;
    [SerializeField]
    float timeBetweenShots = 0.2f;
    [SerializeField]
    float reloadTime = 3f;
    [SerializeField]
    int damage = 3;
    [SerializeField]
    [Range(0f, 100f)]
    float hitChange = 65f;
    float shootTimer = 0f;
    int ammo;

    #endregion

    EnemyMovement movement;

    private void Start() {
        movement = GetComponent<EnemyMovement>();
        ammo = clipSize;
    }

    private void Update() {
        shootTimer += Time.deltaTime;

        // When enemy has ammo and timer is greater than timeBetweenShots
        // and player is in range, shoot towards player
        if (ammo > 0 && shootTimer >= timeBetweenShots && movement.GetPlayerInRange()) {
            Shoot();
        }

        // When ammo has reach 0 and timer is greater than reloadTime
        // set enemy ammo to match clipSize
        if (ammo <= 0 && shootTimer >= reloadTime) {
            ammo = clipSize;
        }
    }

    // Shoot toward player
    private void Shoot() {
        shootTimer = 0f;
        ammo--;
        Debug.Log(this.name + " shoots");

        // When hitChange is greater than random, damage player
        if (Random.Range(0f, 100f) <= hitChange) {
            DamagePlayer();
        }

        // When ammo has reach 0 reload gun
        if (ammo <= 0) Debug.Log(this.name + " reloads gun");
    }

    // Damage player
    private void DamagePlayer() {
        Debug.Log(this.name + " hits player");
    }
}
