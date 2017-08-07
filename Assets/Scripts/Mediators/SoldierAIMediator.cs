using System;
using UnityEngine;

[RequireComponent(typeof(SoldierAIController))]
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

        character.DestroyGO += OnDestroyGObject;
    }

    protected override void OnRemove() {
        controller.Move -= OnMove;
        controller.Jump -= OnJump;
        controller.Fire -= OnFire;
        controller.ChangeWeapons -= OnChangeWeapons;

        character.DestroyGO -= OnDestroyGObject;
    }
}