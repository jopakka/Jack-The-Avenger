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

    float _timer;
    MeleeEnemyMovement _movement;
    GameObject _player;
    Animator _anim;

    #endregion

    private void Awake() {
        _movement = GetComponent<MeleeEnemyMovement>();
        _player = _movement.player;
        _anim = GetComponent<Animator>();
    }

    private void Update() {
        _timer += Time.deltaTime;

        // When timer is greater than timeBetweenAttacks and player is in range
        // attack player
        if (_timer >= _timeBetweenAttacks && _movement.playerInSight) {
            Attack();
        }
    }

    private void Attack() {
        _timer = 0f;

        //Debug.Log(this.name + " tries to hit player");

        _anim.SetTrigger("Hit");

        if (Random.Range(0f, 100f) <= _hitChange) DamagePlayer();
    }

    private void DamagePlayer() {
        //Debug.Log(this.name + " harms the player");
    }
}
