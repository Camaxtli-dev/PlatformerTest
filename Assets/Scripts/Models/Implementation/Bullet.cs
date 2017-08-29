using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet, IGObject {
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _damage;

    [SerializeField]
    protected GameObject Explosion;

    public event Action DestroyGO = delegate { };

#region Implementation interface
    public float Speed {
        get { return _speed; }
        set { _speed = value; }
    }

    public float Damage {
        get { return _damage; }
        set { _damage = value; }
    }
    #endregion

    public void Collider2DEnter(Collider2D collider) {
        if (collider.gameObject.tag != "Waypoint") {
            if (Explosion != null) {
                new ReuseObjectInPoolCommand().Execute(Explosion, transform);
            }
            DestroyGO();
        }
    }

    protected virtual void Awake() {
        new CreateObjectInPoolCommand().Execute(Explosion, 3);
    }
}
