using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
    GameObject player;
    NavMeshAgent nav;
    bool playerInRange;
    float lookSpeed = 10f;
    Vector3 playerLastKnownLocation;
    Vector3 oldDestination;
    bool findPlayer;

    protected virtual void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;
    }

    protected virtual void LookForward() {
        Quaternion look = Quaternion.LookRotation(nav.velocity.normalized);
        look = Quaternion.Euler(new Vector3(0f, look.eulerAngles.y, 0f));
        transform.rotation = Quaternion.Slerp(transform.rotation, look, lookSpeed * Time.deltaTime);
    }

    protected virtual void LookPlayer() {
        Quaternion look = Quaternion.LookRotation(player.transform.position - transform.position);
        look = Quaternion.Euler(new Vector3(0f, look.eulerAngles.y, 0f));
        transform.rotation = Quaternion.Slerp(transform.rotation, look, lookSpeed * Time.deltaTime);
        playerLastKnownLocation = player.transform.position;
        if(!findPlayer) oldDestination = nav.destination;
        findPlayer = true;
    }

    protected virtual void GoToPlayersLastKnowLocation() {
        if(!playerInRange && findPlayer) {
            SetDestination(playerLastKnownLocation);
        }
    }

    protected virtual void GoToOldDestination() {
        SetDestination(oldDestination);
    }

    #region Getters

    public virtual bool GetPlayerInRange() {
        return playerInRange;
    }

    public virtual GameObject GetPlayer() {
        return player;
    }

    public virtual NavMeshAgent GetNavMeshAgent() {
        return nav;
    }

    public virtual Vector3 GetPlayerLastKnownLocation() {
        return playerLastKnownLocation;
    }

    public virtual bool GetFindPlayer() {
        return findPlayer;
    }

    #endregion

    #region Setters

    public virtual void SetPlayerInRange(bool value) {
        playerInRange = value;
    }

    protected virtual void SetDestination(Vector3 destination) {
        nav.SetDestination(destination);
    }

    public virtual void SetFindPlayer(bool value) {
        findPlayer = value;
    }

    #endregion
}
