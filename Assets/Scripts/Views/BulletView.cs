using System;
using System.Collections;
using UnityEngine;

public class BulletView : BaseView, IGObjectView {

    protected Vector3 forward;
    public event Action<Collider2D> ColliderEnter = delegate { };
    public event Action DestroyGO = delegate { };

    protected virtual void Awake() {
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        ColliderEnter(collider);
    }
    
    public virtual void Move(Vector2 newPosition,float speed) {
        forward = new Vector3(transform.forward.x, transform.forward.y, 0);
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + newPosition, speed * Time.deltaTime);
    }

    public override void OnObjectReuse() {
        base.OnObjectReuse();
        StartCoroutine("DestroyCoroutine", 5);
    }

    private IEnumerator DestroyCoroutine(float t) {
        yield return new WaitForSeconds(t);
        DestroyGO();
    }
}