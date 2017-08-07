
using System;
using UnityEngine;

public interface ICharacter {
    float Health { get; set; }
    float Speed { get; set; }
    float JumpPower { get; set; }

    WeaponView[] Guns { get; set; }
    WeaponView CurrentGun { get; set; }

    void IncomingDamage(float damage);
    void ChangeWeapons();

    event Action DestroyGO;
}
