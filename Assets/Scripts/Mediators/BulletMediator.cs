using UnityEngine;

[RequireComponent(typeof(Bullet))]
[RequireComponent(typeof(BulletView))]
[RequireComponent(typeof(BulletController))]
public class BulletMediator : Mediator{

    private Bullet bullet;
    private BulletView view;
    private BulletController controller;

    protected override void Awake() {
        bullet = GetComponent<Bullet>();
        view = GetComponent<BulletView>();
        controller = GetComponent<BulletController>();
        base.Awake();
    }

    protected override void OnRegister() {
        base.OnRegister();
        controller.Move += OnMove;

        view.ColliderEnter += OnObjectColliderEnter;
        view.DestroyGO += OnDestroyGObject;

        bullet.DestroyGO += OnDestroyGObject;
    }

    protected override void OnRemove() {
        base.OnRemove();
        controller.Move -= OnMove;

        view.ColliderEnter -= OnObjectColliderEnter;
        view.DestroyGO -= OnDestroyGObject;
        
        bullet.DestroyGO -= OnDestroy;
    }

    private void OnMove(Vector2 pos) {
        new MoveObjectCommand().Execute(view, pos, bullet.Speed);
    }

    private void OnObjectColliderEnter(Collider2D collision) {
        new ObjectCollider2DEnterCommand().Execute(collision, bullet);    
    }

    private void OnDestroyGObject() {
        new DestroyObjectInPoolCommand().Execute(view);
    }
}