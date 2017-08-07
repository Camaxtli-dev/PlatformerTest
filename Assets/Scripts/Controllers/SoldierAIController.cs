using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAIController : MonoBehaviour {
    public event Action<Vector2> Move = delegate { };
    public event Action Jump = delegate { };
    public event Action Fire = delegate { };
    public event Action ChangeWeapons = delegate { };

    void Start() {
        ChangeWeapons();
    }
}
