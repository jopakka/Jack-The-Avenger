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
    [SerializeField]
    float _range = 10f;
    float _shootTimer = 0f;
    int _ammo;
    GameObject _barrelEnd;

    #endregion

    #region Misc Varibles

    EnemyMovement _movement;
    LineRenderer _gunLine;
    Ray _shootRay;
    int _shootableMask;
    RaycastHit _shootHit;
    float _lineDisplayTime = 0.2f;
    Animator _anim;

    #endregion

    private void Start() {
        _movement = GetComponent<EnemyMovement>();
        _ammo = _clipSize;
        _gunLine = GetComponent<LineRenderer>();
        _shootableMask = LayerMask.GetMask("Player");
        _barrelEnd = GetComponent<Gun>().gun.transform.GetChild(0).transform.Find("BarrelEnd").gameObject;
        _anim = GetComponent<Animator>();
    }

    private void Update() {
        if (PlayerHealth.IsDead) return;

        _shootTimer += Time.deltaTime;

        // When enemy has ammo and timer is greater than timeBetweenShots
        // and player is in range, shoot towards player
        if (_ammo > 0 && _shootTimer >= _timeBetweenShots && _movement.playerInSight) {
            _anim.SetTrigger("Shoot");
            _shootTimer = 0f;
        }

        // When ammo has reach 0 and timer is greater than reloadTime
        // set enemy ammo to match clipSize
        if (_ammo <= 0 && _shootTimer >= _reloadTime) {
            _ammo = _clipSize;
        }

        if (_shootTimer >= _timeBetweenShots * _lineDisplayTime) {
            DisableEffects();
        }
    }

    public void DisableEffects() {
        _gunLine.enabled = false;
    }

    // Shoot toward player
    private void Shoot() {
        _ammo--;
        //Debug.Log(this.name + " shoots");

        // When hitChange is greater than random, damage player
        if (Random.Range(0f, 100f) <= _hitChange) {
            DrawLine(false);
            DamagePlayer();
        } else {
            DrawLine(true);
        }

        // When ammo has reach 0 reload gun
        if (_ammo <= 0) {
            //Debug.Log(this.name + " reloads gun");
            _anim.SetTrigger("Reload");
        }
    }

    // Line renderer
    private void DrawLine(bool _miss) {
        _gunLine.enabled = true;
        _gunLine.SetPosition(0, _barrelEnd.transform.position);
        _shootRay.origin = _barrelEnd.transform.position;
        _shootRay.direction = _barrelEnd.transform.forward - _barrelEnd.transform.up * 0.2f;

        if (Physics.Raycast(_shootRay, out _shootHit, _range, _shootableMask)) {
            if (_miss) {
                _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * _range);
            } else {
                _gunLine.SetPosition(1, _shootHit.point);
            }
        } else {
            _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * _range);
        }
    }

    // Damage player
    private void DamagePlayer() {
        //Debug.Log(this.name + " hits player");
        PlayerHealth ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        ph.TakeDamage(_damage);
    }
}
