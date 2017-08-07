using UnityEngine;

public class CreateObjectInPoolCommand {
    public void Execute(GameObject poolObject, int count) {
        PoolManager.Instance.CreatePool(poolObject, count);
    }
}

public class ReuseObjectInPoolCommand {
    public GameObject Execute(GameObject poolObject) {
        return PoolManager.Instance.GetReuseObject(poolObject, Vector3.zero, Quaternion.identity);
    }
    public GameObject Execute(GameObject poolObject, Transform transform) {
        return PoolManager.Instance.GetReuseObject(poolObject, transform.position, transform.rotation);
    }
}

public class DestroyObjectInPoolCommand{
    public void Execute(PoolObject poolObject) {
        poolObject.Destroy();
    }
}

public class MoveObjectCommand {
    public void Execute(IGObjectView view, Vector2 newPosition, float speed) {
        view.Move(newPosition, speed);
    }
}

public class ObjectCollider2DEnterCommand {
    public void Execute(Collider2D collider, IGObject obj) {
        obj.Collider2DEnter(collider);
    }
}