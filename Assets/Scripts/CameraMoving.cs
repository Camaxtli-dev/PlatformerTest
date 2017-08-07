using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour {
    [SerializeField]
    private Transform target;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    void Update () {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
	}
}
