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

        if (ammo > 0 && shootTimer >= timeBetweenShots && movement.GetPlayerInRange()) {
            Shoot();
        }

        if (ammo <= 0 && shootTimer >= reloadTime) {
            ammo = clipSize;
        }
    }

    private void Shoot() {
        shootTimer = 0f;
        ammo--;
        Debug.Log(this.name + " shoots");

        if (Random.Range(0f, 100f) <= hitChange) {
            DamagePlayer();
        }

        if (ammo <= 0) Debug.Log(this.name + " reload gun");
    }

    private void DamagePlayer() {
        Debug.Log(this.name + " hits player");
    }
}
