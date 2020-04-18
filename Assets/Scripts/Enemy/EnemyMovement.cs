using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    #region Variables

    GameObject _player;
    NavMeshAgent _nav;
    bool _playerInRange;
    float _lookSpeed = 10f;
    Vector3 _playerLastKnownLocation;
    Vector3 _oldDestination;
    bool _findPlayer;

    #endregion

    protected virtual void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
        _nav = GetComponent<NavMeshAgent>();
        _nav.updateRotation = false;
    }

    // Sets rotation to forward
    protected virtual void LookForward() {
        Quaternion look = Quaternion.LookRotation(_nav.velocity.normalized);
        look = Quaternion.Euler(new Vector3(0f, look.eulerAngles.y, 0f));
        transform.rotation = Quaternion.Slerp(transform.rotation, look, _lookSpeed * Time.deltaTime);
    }

    // Sets rotation to player position
    protected virtual void LookPlayer() {
        Quaternion look = Quaternion.LookRotation(_player.transform.position - transform.position);
        look = Quaternion.Euler(new Vector3(0f, look.eulerAngles.y, 0f));
        transform.rotation = Quaternion.Slerp(transform.rotation, look, _lookSpeed * Time.deltaTime);
        _playerLastKnownLocation = _player.transform.position;
        if(!_findPlayer) _oldDestination = _nav.destination;
        _findPlayer = true;
    }

    // Sets new destination to players last known location
    protected virtual void GoToPlayersLastKnowLocation() {
        if(!_playerInRange && _findPlayer) {
            destination = _playerLastKnownLocation;
        }
    }

    // Sets destination back to where enemy left
    protected virtual void GoToOldDestination() {
        destination = _oldDestination;
    }


    #region Getters Setters

    public virtual bool playerInRange {
        get { return _playerInRange; }
        set { _playerInRange = value; }
    }

    public virtual GameObject player {
        get { return _player; }
    }

    public virtual NavMeshAgent navMeshAgent {
        get { return _nav; }
    }

    public virtual Vector3 playerLastKnownLocation {
        get { return _playerLastKnownLocation; }
    }

    public virtual bool findPlayer {
        get { return _findPlayer; }
        set { _findPlayer = value; }
    }

    public virtual Vector3 destination {
        get { return _nav.destination; }
        protected set { _nav.SetDestination(value); }
    }

    #endregion
}
