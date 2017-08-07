using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerView))]
[RequireComponent(typeof(Character))]
public class PlayerMediator : Mediator {

    private PlayerView view;
    private PlayerController controller;
    private Character character;

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

    private void OnMove(Vector2 pos) {
        new MoveObjectCommand().Execute(view, pos, character.Speed);
    }

    private void OnJump() { }

    private void OnFire() {
        new CharacterFireCommand().Execute(view.currentGun.GetComponent<Weapon>());
    }

    private void OnChangeWeapons() {
        new CharacterChangeWeaponsCommand().Execute(character, view);
    }

    private void OnIncomingDamage(float damage) {
        //  Character.Health -= damage;
    }
    private void OnDestroyGObject() {
        new DestroyObjectInPoolCommand().Execute(view);
    }
}
