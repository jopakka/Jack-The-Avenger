using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            ShootPlayer();
        }

        if (ammo <= 0 && shootTimer >= reloadTime) {
            ammo = clipSize;
        }
    }

    private void ShootPlayer() {
        shootTimer = 0f;
        Debug.Log(this.name + " shoots player");
        ammo--;

        if (ammo <= 0) Debug.Log(this.name + " reload gun");
    }
}
