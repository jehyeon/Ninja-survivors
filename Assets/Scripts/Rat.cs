using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MeleeEnemy
{
    private void Start()
    {
        canFly = false;
        
        attackRange = 2f;

        _stat.Hp = 10;
        _stat.Speed = 5;
        _stat.Damage = 2;

        exp = 10;
    }

    protected override void Update()
    {
        base.Update();

        // isAttack Trigger 시 Weapon collider 활성화
        ActivateWeapon();
    }
}
