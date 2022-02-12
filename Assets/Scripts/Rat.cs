using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MeleeEnemy
{
    private void Start()
    {
        enemyOP = GameObject.Find("Rat Object Pool").GetComponent<ObjectPool>();
        canFly = false;
        
        attackRange = 2f;

        _stat.Hp = 20;
        _stat.Speed = 6;
        _stat.Damage = 3;

        exp = 10;
    }

    protected override void Update()
    {
        base.Update();

        // isAttack Trigger 시 Weapon collider 활성화
        ActivateWeapon();
    }
   protected new void ActivateWeapon()
    {
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
        {
            enemyWeaponCollider.enabled = true;
        }
        else
        {
            enemyWeaponCollider.enabled = false;
        }
    }    
}
