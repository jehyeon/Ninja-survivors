using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MeleeEnemy
{
    private void Start()
    {
        canFly = false;
        
        attackRange = 2f;

        _stat.Hp = 30;
        _stat.Speed = 5;
        _stat.Damage = 5;

        exp = 50;
    }

    protected override void Update()
    {
        base.Update();

        // isAttack Trigger 시 Weapon collider 활성화
        ActivateWeapon();
    }
}
