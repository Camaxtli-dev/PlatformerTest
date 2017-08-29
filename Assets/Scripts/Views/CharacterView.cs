using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : PoolObject, IGObjectView {

    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject model;
    [SerializeField] protected float smoothing = 10f;
    [SerializeField] protected Transform gunPosition;
    [SerializeField] protected Transform rightShoulder;
    [SerializeField] protected Transform shoulderTrans; // GunPosition?

    public Transform groundedEnd1;
    public Transform groundedEnd2;
    public Vector3 targetPosition;
    public Vector3 nonTargetPosition;
    public WeaponView currentGun;

    protected float move;
    protected bool isJumping = false;
    protected GameObject rsp;
    public GameObject target;

    protected virtual void Start() {
        rsp = new GameObject();
        rsp.name = transform.root.name + " Right Shoulder IK Helper";
    }

    protected virtual void FixedUpdate() {
        HandlerAnimation();
        HandlerAimingPos();
        HandlerRotation();
        HandlerShoulder();
    }

    protected virtual void HandlerAimingPos() { /* Модель игрока следит за курсором, бот смотрит вперед или берет в таргет игрока и следит за ним. */ }

    protected virtual void HandlerRotation() {
        Vector3 directionToLook = targetPosition - model.transform.position;
        directionToLook.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);

        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, targetRotation, Time.deltaTime * smoothing);
    }

    protected virtual void HandlerShoulder() {
        shoulderTrans.LookAt(targetPosition);

        Vector3 rightShoulderPos = rightShoulder.TransformPoint(Vector3.zero);
        rsp.transform.position = rightShoulderPos;
        rsp.transform.parent = transform;

        shoulderTrans.position = rsp.transform.position;
    }

    protected virtual void HandlerAnimation() {
        anim.SetBool("Jump", IsOnGround());

        float animValue = move; // Пофиксить должна быть отдельная переменная
        if (targetPosition.x < transform.position.x) {
            animValue = -animValue;
        }
        anim.SetFloat("Move", animValue, .1f, Time.deltaTime);
    }

    protected virtual bool IsOnGround() {
        if (Physics2D.Linecast(transform.position, groundedEnd1.position, 1 << LayerMask.NameToLayer("Ground")))
            return false;
        if (Physics2D.Linecast(transform.position, groundedEnd2.position, 1 << LayerMask.NameToLayer("Ground")))
            return false;

        return true;
    }

    public virtual void Move(Vector2 newPosition, float speed) {
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + newPosition, speed * Time.deltaTime);
    }

    public virtual void ChangeWeapons(WeaponView gun) {
        if (currentGun != null) {
            currentGun.Destroy();
            currentGun = null;
            Debug.Log("Destroy");
        }
        if (currentGun == null && gun != null) {
            /* Исправить баг с положением рук, добавить на оружие точки расположения рук, и привязывать руки к этим точкам
             * IKHandler.RighrHandTarget = Weapon.RightHandHold;
             * IKHandler.LeftHandTarget = Weapon.LeftHandHold;
             */
            Debug.Log("ChangeWeapons");
            currentGun = new ReuseObjectInPoolCommand().Execute(gun.gameObject).GetComponent<WeaponView>();
            currentGun.transform.parent = gunPosition;
            currentGun.transform.localRotation = Quaternion.identity;
            currentGun.transform.localPosition = new Vector3(0, 0, 0.5f);
            currentGun.transform.localScale = Vector3.one;
        } else {
            Debug.Log(currentGun);
        }
    }

    protected virtual IEnumerator Jump() {
        isJumping = true;
        //new CharacterJumpCommand(this, Character.JumpPower).Execute();
        yield return new WaitForSeconds(0.3f);
        isJumping = false;
    }
}
