using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MeleeEnemy
{
    private void Start()
    {
        enemyOP = GameObject.Find("Crab Object Pool").GetComponent<ObjectPool>();
        canFly = false;
        
        attackRange = 3f;

        _stat.Hp = 15;
        _stat.Speed = 3;
        _stat.Damage = 10;

        exp = 20;
    }

    protected override void Update()
    {
        base.Update();

        // isAttack Trigger 시 Weapon collider 활성화
        ActivateWeapon();
    }
}