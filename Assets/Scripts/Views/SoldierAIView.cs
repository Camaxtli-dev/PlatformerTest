using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAIView : CharacterView {
    public Transform positionGunNonTarget;

    protected override void HandlerAimingPos() {
        if (target != null && target.activeSelf == true) {
            Vector3 targetPos;
            if (target.transform.Find("AimingPoint") != null) {
                targetPos = target.transform.Find("AimingPoint").position;
            } else {
                if (target.transform.position.x < transform.position.x) {
                    targetPos = new Vector3(positionGunNonTarget.position.x * -1, positionGunNonTarget.position.y, 0);
                } else {
                    targetPos = new Vector3(positionGunNonTarget.position.x * 1, positionGunNonTarget.position.y, 0);
                }
            }
            targetPos.z = transform.position.z;
            targetPosition = targetPos;
        }
    }
}
