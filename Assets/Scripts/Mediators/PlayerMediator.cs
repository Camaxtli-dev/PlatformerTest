using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerView))]
[RequireComponent(typeof(Character))]
public class PlayerMediator : CharacterMediator {
    
    private PlayerController controller;

    protected override void Awake() {
        view = GetComponent<PlayerView>();
        controller = GetComponent<PlayerController>();
        character = GetComponent<Character>();
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
