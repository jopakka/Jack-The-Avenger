using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField]
    GameObject _enemy;
    [SerializeField]
    int _amount = 3;
    [SerializeField]
    float _spawnRate = 1f;
    [SerializeField]
    List<SpawnPoint> _spawnPoints;

    //PlayerHealth playerHealth;
    int _spawnCounter = 0;
    bool _stopped;

    private void Start() {
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        InvokeRepeating("Spawn", 0, _spawnRate);
    }

    private void Update() {
        if (!_stopped) {
            if (_spawnCounter >= _amount) {
                CancelInvoke();
                _stopped = true;
            }
        }
    }

    private void Spawn() {
        //if (playerHealth.currentHealth <= 0) return;

        int _spawnPointIndex = Random.Range(0, _spawnPoints.Count);

        Instantiate(_enemy, _spawnPoints[_spawnPointIndex].transform.position, _spawnPoints[_spawnPointIndex].transform.rotation);

        _spawnCounter++;
    }
}
