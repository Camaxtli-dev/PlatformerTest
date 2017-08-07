using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mediator : MonoBehaviour {

    protected virtual void Awake() { OnRegister(); }

    protected virtual void OnDestroy() { OnRemove(); }

    protected virtual void OnRegister() { }

    protected virtual void OnRemove() { }
}
