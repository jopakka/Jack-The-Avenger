using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyShooting : MonoBehaviour {

    #region Gun variables

    [SerializeField]
    int _clipSize = 30;
    [SerializeField]
    float _timeBetweenShots = 0.2f;
    [SerializeField]
    float _reloadTime = 3f;
    [SerializeField]
    int _damage = 3;
    [SerializeField]
    [Range(0f, 100f)]
    float _hitChange = 65f;
    float _shootTimer = 0f;
    int _ammo;

    #endregion

    EnemyMovement _movement;

    private void Start() {
        _movement = GetComponent<EnemyMovement>();
        _ammo = _clipSize;
    }

    private void Update() {
        _shootTimer += Time.deltaTime;

        // When enemy has ammo and timer is greater than timeBetweenShots
        // and player is in range, shoot towards player
        if (_ammo > 0 && _shootTimer >= _timeBetweenShots && _movement.playerInRange) {
            Shoot();
        }

        // When ammo has reach 0 and timer is greater than reloadTime
        // set enemy ammo to match clipSize
        if (_ammo <= 0 && _shootTimer >= _reloadTime) {
            _ammo = _clipSize;
        }
    }

    // Shoot toward player
    private void Shoot() {
        _shootTimer = 0f;
        _ammo--;
        Debug.Log(this.name + " shoots");

        // When hitChange is greater than random, damage player
        if (Random.Range(0f, 100f) <= _hitChange) {
            DamagePlayer();
        }

        // When ammo has reach 0 reload gun
        if (_ammo <= 0) Debug.Log(this.name + " reloads gun");
    }

    // Damage player
    private void DamagePlayer() {
        Debug.Log(this.name + " hits player");
    }
}
