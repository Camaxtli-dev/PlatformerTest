using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : CharacterView {
    [SerializeField] protected Camera cameraTransform;
        
    protected override void Start() {
        base.Start();
        cameraTransform = GameObject.Find("Camera").GetComponent<Camera>();
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
        move = Input.GetAxisRaw("Horizontal");
    }
    protected override void HandlerAimingPos() {
        Ray ray = cameraTransform.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            Vector3 lookP = hit.point;
            lookP.z = transform.position.z;
            lookPos = lookP;
        }
    }    
}
