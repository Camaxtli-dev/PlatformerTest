using UnityEngine;

public class PoolObject : CachedComponents {

    public virtual void OnObjectReuse() { }

    public void Destroy() {
        gameObject.SetActive(false);
    }
}
