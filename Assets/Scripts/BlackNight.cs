using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackNight : MeleeEnemy
{
    private void Start()
    {
        canFly = false;
        
        attackRange = 3f;

        _stat.Hp = 1;
        _stat.Speed = 3;
        _stat.Damage = 1;

        exp = 100;
    }

    protected override void Update()
    {
        base.Update();

        // isAttack Trigger 시 Weapon collider 활성화
        ActivateWeapon();
    }
}
