using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon {
    private float _attackSpeed;

    [SerializeField]
    protected GameObject bullet;
    [SerializeField]
    protected Transform positionOfShot;

    public bool isReload = false;

    #region Implementation interface
    public float AttackSpeed {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }

    public virtual void Fire() {
        if (!isReload)
            StartCoroutine("FireCoroutine");
    }
    #endregion

    void Awake() {
        AttackSpeed = 0.1f;
        new CreateObjectInPoolCommand().Execute(bullet, 3);
    }

    protected virtual IEnumerator FireCoroutine() {
        isReload = true;
        new ReuseObjectInPoolCommand().Execute(bullet, positionOfShot);
        yield return new WaitForSeconds(AttackSpeed);
        isReload = false;
    }
}
