using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] protected Camera cameraTransform;
    [SerializeField] protected Texture2D cursor;

    private GameObject target;

    public event Action<Vector2> Move = delegate { };
    public event Action Jump = delegate { };
    public event Action Fire = delegate { };
    public event Action ChangeWeapons = delegate { };
    public event Action<GameObject> OnTarget = delegate { };
    public event Action<Vector3> LookPosition = delegate { };

    protected void Start() {
        ChangeWeapons();
        cameraTransform = GameObject.Find("Camera").GetComponent<Camera>();
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    protected void FixedUpdate() {
        HandlerMovement();
        HandleTarget();
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

    protected void HandleTarget() {
        if (target == null) {
            Vector3 targetPos = cameraTransform.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = transform.position.z;
            LookPosition(targetPos);
        } else {
            OnTarget(target);
        }
    }
}
