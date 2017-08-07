using System;
using UnityEngine;

public class Character : MonoBehaviour, ICharacter {
    private float _health = 100;
    private float _speed = 3;
    private float _jumpPower = 5;

    [SerializeField]
    private WeaponView[] guns;
    private WeaponView currentGun;
    private int weapNum;

    public event Action DestroyGO = delegate { };

    #region Implementation interface
    public float Health {
        get { return _health; }
        set { _health = value; }
    }

    public float Speed {
        get { return _speed; }
        set { _speed = value; }
    }

    public float JumpPower {
        get { return _jumpPower; }
        set { _jumpPower = value; }
    }

    public WeaponView[] Guns {
        get { return guns; }
        set { guns = value; }
    }

    public WeaponView CurrentGun {
        get { return currentGun; }
        set { currentGun = value; }
    }

    public void IncomingDamage(float damage) {
        Health -= damage;
        if(Health <= 0) {
            DestroyGO();
        }
    }

    public void ChangeWeapons() {
        if (Guns.Length > weapNum) {
            CurrentGun = Guns[weapNum++];
        } else {
            weapNum = 0;
            CurrentGun = Guns[weapNum++];
        }
    }
    #endregion

    protected void Start() {
        for (int i = 0; i < Guns.Length; i++) {
            new CreateObjectInPoolCommand().Execute(Guns[i].gameObject, 1);
        }
    }
}
