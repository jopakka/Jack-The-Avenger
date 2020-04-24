using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour{

    #region Variables

    [SerializeField]
    int _maxHealth = 100;
    int _currentHealth;
    bool _isDead;
    float _sinkSpeed = 0.5f;
    CapsuleCollider _capsuleCollider;
    EnemyMovement _movement;
    NavMeshAgent _navMeshAgent;
    Rigidbody _rb;
    Animator _anim;
    EnemyShooting _shooting;
    bool _isSinking;

    #endregion

    private void Start() {
        _currentHealth = _maxHealth;
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _movement = GetComponent<EnemyMovement>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _shooting = GetComponent<EnemyShooting>();
    }

    private void Update() {
        if (_isSinking) {
            transform.Translate(-Vector3.up * _sinkSpeed * Time.deltaTime);
        }
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
        _movement.enabled = false;
        _navMeshAgent.enabled = false;
        _shooting.enabled = false;
        _anim.SetTrigger("Die");
    }

    public void StartSinking() {
        _isSinking = true;
        Destroy(gameObject, 2f);
    }

    public int currentHealth {
        get { return _currentHealth; }
    }
}