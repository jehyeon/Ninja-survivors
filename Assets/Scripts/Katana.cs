using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon
{
    [SerializeField]
    private GameObject attackRange;
    private BoxCollider attackCollider;

    public GameObject AttackRange { get { return attackRange; } }

    private void Start()
    {
        stat = go_player.GetComponent<Stat>();
        attackAbilityCommand = go_player.GetComponent<AttackAbilityCommand>();
        attackCollider = attackRange.GetComponent<BoxCollider>();
        
        attackCollider.enabled = false;
        UpdateAttackCooltime();
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
        // 수평 범위 collider 활성화
        attackCollider.transform.rotation = Quaternion.Euler(
            attackCollider.transform.rotation.eulerAngles.x, 
            attackCollider.transform.rotation.eulerAngles.y, 
            0
        );
        StartCoroutine(AttackColliderCoroutine(attackType));
    }

    public override void AttackOnAir(int attackType)
    {
        // 수직 범위 collider 활성화
        attackCollider.transform.rotation = Quaternion.Euler(
            attackCollider.transform.rotation.eulerAngles.x, 
            attackCollider.transform.rotation.eulerAngles.y, 
            90
        );
        StartCoroutine(AttackColliderCoroutine(attackType));
    }

    public override void AttackSuccess(Collider collider)
    {
        // 공격 범위 collider에서 호출
        
        // 더블공격
        if (Random.value < stat.DoublePercent)
        {
            Attack(collider);
        }
        Attack(collider);
    }

    IEnumerator AttackColliderCoroutine(int attackType)
    {
        yield return new WaitForSeconds(attackCooltime / 3f);   // attack start delay
        attackCollider.enabled = true;
        yield return new WaitForSeconds(attackCooltime / 3f);
        attackCollider.enabled = false;
        yield break;
    }
    
    public override void UpgradeAttackRange(int level = 1)
    {
        // Level당 10% 씩 범위 증가
        for (int i = 0; i < level; i++)
        {
            attackCollider.size += new Vector3(attackCollider.size.x * 0.1f, 0, attackCollider.size.z * 0.1f);
            attackCollider.center = new Vector3(0, 0, attackCollider.size.z / 2f);
        }
    }

    public void ActivateCollider()
    {
        attackCollider.enabled = true;
    }

    public void DeActivateCollider()
    {
        attackCollider.enabled = false;
    }

    public override void UpdateAttackCooltime()
    {
        // katana default attack animation speed is 1.133s
        attackCooltime = 1.133f / stat.AttackSpeed;
    }
}
