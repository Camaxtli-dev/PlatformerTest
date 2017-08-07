using UnityEngine;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(CharacterView))]
public class CharacterMediator : Mediator {
    protected ICharacter character;
    protected CharacterView view;

    protected override void Awake() {
        base.Awake();
    }
    
    protected virtual void OnMove(Vector2 pos) {
        new MoveObjectCommand().Execute(view, pos, character.Speed);
    }

    protected virtual void OnJump() {
        // new CharacterJumpCommand().Execute(view);
    }

    protected virtual void OnFire() {
        new CharacterFireCommand().Execute(view.currentGun.GetComponent<Weapon>());
    }

    protected virtual void OnChangeWeapons() {
        new CharacterChangeWeaponsCommand().Execute(character, view);
    }

    protected virtual void OnIncomingDamage(float damage) {
        // new CharacterIncomingDamageCommand().Execute(character, damage);
    }
    protected virtual void OnDestroyGObject() {
        new DestroyObjectInPoolCommand().Execute(view);
    }
}