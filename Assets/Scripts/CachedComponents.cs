using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CachedComponents : MonoBehaviour {
    private Transform _transform = null;
    private bool _transformCached = false;

    private Rigidbody _rigidbody = null;
    private bool _rigidbodyCached = false;

    private Rigidbody2D _rigidbody2D = null;
    private bool _rigidbody2DCached = false;

    public Transform transform {
        get {
            if (!_transformCached) {
                _transformCached = true;
                _transform = GetComponent<Transform>();
            }
            return _transform;
        }
    }
    public Rigidbody rigidbody {
        get {
            if (!_rigidbodyCached) {
                _rigidbodyCached = true;
                _rigidbody = GetComponent<Rigidbody>();
            }
            return _rigidbody;
        }
    }
    public Rigidbody2D rigidbody2D {
        get {
            if (!_rigidbody2DCached) {
                _rigidbody2DCached = true;
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
            return _rigidbody2D;
        }
    }
}
