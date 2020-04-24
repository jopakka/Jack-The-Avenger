using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour{
    [SerializeField]
    GameObject _gun;
    [SerializeField]
    GameObject _gunSpot;

    private void Update() {
        _gun.transform.position = _gunSpot.transform.position;
        _gun.transform.rotation = _gunSpot.transform.rotation;
    }

    public GameObject gun {
        get { return _gun; }
    }
}
