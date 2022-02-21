using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon
{
    [SerializeField]
    private GameObject attackRange;
    private BoxCollider attackCollider;

    public float duration = .5f;
    private void Start()
    {
        stat = go_player.GetComponent<Stat>();
        attackAbilityCommand = go_player.GetComponent<AttackAbilityCommand>();
        attackCollider = attackRange.GetComponent<BoxCollider>();
        
        attackCollider.enabled = false;
    }

    private void InitCollider()
    {
    }

    private void Attack(Collider other)
    {
        // 크리티컬
        if (Random.value < stat.CriticalPercent)
        {
            // !!! 이펙트 추가하기
            attackAbilityCommand.CriticalAttack();      // 치명타 공격 어빌리티
            other.gameObject.GetComponent<Enemy>().GetDamage(stat.Damage * 2);
        }
        else
        {
            attackAbilityCommand.Attack();              // 공격 시 일정확률 어빌리티
            other.gameObject.GetComponent<Enemy>().GetDamage(stat.Damage);
        }

        if (stat.AttackHpAbsorption > 0)
        {
            // 타격마다 체력 회복
            stat.Heal(stat.AttackHpAbsorption);
        }
    }

    public override void Attack(int attackType)
    {
        attackCollider.transform.rotation = Quaternion.Euler(
            attackCollider.transform.rotation.eulerAngles.x, 
            attackCollider.transform.rotation.eulerAngles.y, 
            0
        );
        StartCoroutine(AttackColliderCoroutine(attackType, duration));
    }

    public override void AttackOnAir(int attackType)
    {
        attackCollider.transform.rotation = Quaternion.Euler(
            attackCollider.transform.rotation.eulerAngles.x, 
            attackCollider.transform.rotation.eulerAngles.y, 
            90
        );
        StartCoroutine(AttackColliderCoroutine(attackType, duration));
    }

    public override void AttackSuccess(Collider collider)
    {
        // 더블공격
        if (Random.value < stat.DoublePercent)
        {
            Attack(collider);
        }
        Attack(collider);
    }

    IEnumerator AttackColliderCoroutine(int attackType, float duration)
    {
        yield return new WaitForSeconds(.3f);   // attack start delay
        attackCollider.enabled = true;
        yield return new WaitForSeconds(duration);
        attackCollider.enabled = false;
        yield break;
    }
    
    public override void UpgradeAttackRange(int level = 1)
    {
        // Level당 20% 씩 범위 증가
        for (int i = 0; i < level; i++)
        {
            attackCollider.center += new Vector3(0, 0.1f, 0);
            attackCollider.size += new Vector3(0, 0.2f, 0);
        }
    }
}
