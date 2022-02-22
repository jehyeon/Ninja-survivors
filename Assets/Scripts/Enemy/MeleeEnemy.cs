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
    protected override IEnumerator Attack()
    {
        yield return StartCoroutine(base.Attack());
        isAttack = true;
        // enemy 마다 개별적인 딜레이 지정이 필요하면 하위 클래스에서 오버라이드
        yield return new WaitForSeconds(attackCooltime * 0.25f);        // attack start delay
        enemyWeaponCollider.enabled = true;
        yield return new WaitForSeconds(attackCooltime * 0.5f);
        enemyWeaponCollider.enabled = false;
        yield return new WaitForSeconds(attackCooltime * 0.25f);
        isAttack = false;   // isAttack true일 때에는 isMove, isRotate가 false로 고정
    }
}
