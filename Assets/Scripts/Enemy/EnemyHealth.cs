using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour{

    #region Variables

    [SerializeField]
    int _maxHealth = 100;
    int _currentHealth;

    [SerializeField]
    bool _isDead;
    CapsuleCollider _capsuleCollider;
    EnemyMovement _enemyMovement;
    NavMeshAgent _navMeshAgent;
    Rigidbody _rb;

    #endregion

    private void Start() {
        _currentHealth = _maxHealth;
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (_isDead) Death();
    }

    public void TakeDamage(int amount) {
        if (_isDead) return;

        _currentHealth -= amount;

        if (_currentHealth <= 0) Death();
    }

    private void Death() {
        _isDead = true;
        _capsuleCollider.isTrigger = true;
        _rb.isKinematic = true;
        _enemyMovement.enabled = false;
        _navMeshAgent.isStopped = true;
        Destroy(gameObject, 2f);
    }

    public int currentHealth {
        get { return _currentHealth; }
    }
}