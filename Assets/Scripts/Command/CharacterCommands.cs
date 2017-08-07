﻿using UnityEngine;

public class CharacterFireCommand {
    public void Execute(IWeapon weapon) {
        weapon.Fire();
    }
}

public class CharacterJumpCommand {
    public void Execute(CharacterController characterController, float jumpPower) {
       // characterController.rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); Это будет выполняться в View
    }
}

public class CharacterIncomingDamageCommand {
    public void Execute(ICharacter character, float damage) {
        character.IncomingDamage(damage);
    }
}

public class CharacterChangeWeaponsCommand {
    public void Execute(ICharacter character, CharacterView view) {
        character.ChangeWeapons();
        view.ChangeWeapons(character.CurrentGun);
    }
}