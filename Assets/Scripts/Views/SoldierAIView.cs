using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAIView : CharacterView {
    public GameObject target;

    protected override void HandlerAimingPos() {
        if (target != null && target.activeSelf == true) {
            Vector3 lookP = target.transform.Find("AimingPoint").position;
            lookP.z = transform.position.z;
            lookPos = lookP;
        } else {
            target = null;
            lookPos = Vector3.zero;
        }
    }
}
