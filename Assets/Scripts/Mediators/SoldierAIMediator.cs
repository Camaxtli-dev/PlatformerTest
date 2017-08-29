using System;
using UnityEngine;

[RequireComponent(typeof(SoldierAI))]
[RequireComponent(typeof(SoldierAIController))]
[RequireComponent(typeof(SoldierAIView))]
public class SoldierAIMediator : CharacterMediator {

    private SoldierAIController controller;

    protected override void Awake() {
        character = GetComponent<Character>();
        view = GetComponent<SoldierAIView>();
        controller = GetComponent<SoldierAIController>();
        base.Awake();
    }

    protected override void OnRegister() {
        controller.Move += OnMove;
        controller.Jump += OnJump;
        controller.Fire += OnFire;
        controller.ChangeWeapons += OnChangeWeapons;
        controller.OnTarget += OnTarget;

        character.DestroyGO += OnDestroyGObject;
    }

    protected override void OnRemove() {
        controller.Move -= OnMove;
        controller.Jump -= OnJump;
        controller.Fire -= OnFire;
        controller.ChangeWeapons -= OnChangeWeapons;
        controller.OnTarget -= OnTarget;

        character.DestroyGO -= OnDestroyGObject;
    }

    private void OnTarget(GameObject target) {
        new CharacterOnTargetController().Execute(target, view);
    }
}