using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour {

    #region Variables

    GameObject _player;
    GameObject _parent;
    float _fieldOfView;
    EnemyMovement _movement;
    SphereCollider _col;

    #endregion

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
        _parent = transform.parent.gameObject;
        _movement = _parent.GetComponent<EnemyMovement>();
        _col = GetComponent<SphereCollider>();
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject == _player) {
            _movement.playerInRange = false;

            Vector3 _direction = new Vector3(_player.transform.position.x - _parent.transform.position.x, 0f,
                _player.transform.position.z - _parent.transform.position.z);
            float angle = Vector3.Angle(_direction, _parent.transform.forward);

            if (Mathf.Abs(angle) <= _fieldOfView) {
                RaycastHit _hit;
                Debug.DrawRay(transform.position + transform.up * 0.6f, _direction.normalized * _col.radius);

                if (Physics.Raycast(transform.position + transform.up * 0.6f, _direction.normalized, out _hit, _col.radius)) {
                    if (_hit.collider.gameObject == _player) {
                        _movement.playerInRange = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == _player) {
            _movement.playerInRange = false;
        }
    }

    public float fieldOfView {
        get { return _fieldOfView; }
        set { _fieldOfView = value / 2f; }
    }
}
