using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : CharacterView {

    protected override void FixedUpdate() {
        base.FixedUpdate();
        move = Input.GetAxisRaw("Horizontal");
    }
    protected override void HandlerAimingPos() {
        /*  Ray ray = cameraTransform.ScreenPointToRay(Input.mousePosition);

          RaycastHit hit;

          if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
              Vector3 lookP = hit.point;
              lookP.z = transform.position.z;
              targetPosition = lookP;
          }*/
        if (target != null) {
            Vector3 targetPos = target.transform.position;
            targetPos.z = transform.position.z;
            targetPosition = targetPos;
        } else {
            targetPosition = nonTargetPosition;
        }
    }
}
