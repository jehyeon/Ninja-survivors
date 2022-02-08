using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MeleeEnemy
{
    private void Start()
    {
        canFly = false;
        
        attackRange = 2f;

        _stat.Hp = 15;
        _stat.Speed = 5;
        _stat.Damage = 3;

        exp = 15;
    }

    protected override void Update()
    {
        base.Update();

        // isAttack Trigger 시 Weapon collider 활성화
        ActivateWeapon();
    }
}