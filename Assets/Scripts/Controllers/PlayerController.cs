using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public event Action<Vector2> Move = delegate { };
    public event Action Jump = delegate { };
    public event Action Fire = delegate { };
    public event Action ChangeWeapons = delegate { };
    
    protected void Start() {
        ChangeWeapons();
    }
    protected void FixedUpdate() {
        HandlerMovement();
    }

    protected void Update() {
        if (Input.GetKey(KeyCode.Q)) {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            ChangeWeapons();
        }
    }

    protected void HandlerMovement() {
        if (Input.GetAxisRaw("Horizontal") != 0) {
            Move(Vector2.right * Input.GetAxisRaw("Horizontal"));
        }

        if (Input.GetAxis("Jump") != 0) {
            Jump();
        }
    }
}
