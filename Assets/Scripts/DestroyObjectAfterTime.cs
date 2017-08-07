using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterTime : PoolObject {

    public float time = 5;

    public override void OnObjectReuse() {
        base.OnObjectReuse();
        StartCoroutine("DestroyCoroutine", time);
    }

    private IEnumerator DestroyCoroutine(float t) {
        yield return new WaitForSeconds(t);
        new DestroyObjectInPoolCommand().Execute(this);
    }
}
