using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizzard : MeleeEnemy
{
    private void Start()
    {
        canFly = false;
        
        attackRange = 2f;

        _stat.Hp = 50;
        _stat.Speed = 5;
        _stat.Damage = 10;

        exp = 100;
    }

    protected override void Update()
    {
        base.Update();

        // isAttack Trigger 시 Weapon collider 활성화
        ActivateWeapon();
    }
}

