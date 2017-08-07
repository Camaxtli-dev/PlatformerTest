using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAIView : CharacterView {
    public GameObject target;
    protected override void HandlerAimingPos() {
        /*
        Ray ray = cameraTransform.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            Vector3 lookP = hit.point;
            lookP.z = transform.position.z;
            lookPos = lookP;
        }*/
        if (target != null) {
            Vector3 lookP = target.transform.position;
            lookP.z = transform.position.z;
            lookPos = lookP;
        }
    }
}
