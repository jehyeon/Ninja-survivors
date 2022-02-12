using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField]
    protected GameObject go_enemyWeapon;   // 직접 넣어줘야 함
    protected BoxCollider enemyWeaponCollider;

    protected override void Awake()
    {
        base.Awake();
        
        enemyWeaponCollider = go_enemyWeapon.GetComponent<BoxCollider>();
    }

    // 무기 collider 활성화
    protected void ActivateWeapon()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            enemyWeaponCollider.enabled = true;
            isMove = false;
        }
        else
        {
            enemyWeaponCollider.enabled = false;
            isMove = true;
        }
    }
}
