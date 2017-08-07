using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : PoolObject {

    public event Action<Vector2> Move = delegate { };

    protected void FixedUpdate() {
        Move(transform.forward);
    }
}
