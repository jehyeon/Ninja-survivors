using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackNight : MeleeEnemy
{
    private void Start()
    {
        canFly = false;
        
        attackRange = 2f;

        _stat.Hp = 200;
        _stat.Speed = 3;
        _stat.Damage = 50;

        exp = 200;
    }

    protected override void Update()
    {
        base.Update();

        // isAttack Trigger 시 Weapon collider 활성화
        ActivateWeapon();
    }
}
