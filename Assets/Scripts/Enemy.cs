using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // this
    protected bool isMove;
    protected bool canAttack;     // 공격 범위 안에 있는지
    protected float attackRange;
    protected bool canFly;    // 공중 유닛이면 true
    protected int exp;
    protected Vector3 dir;

    // target
    protected GameObject player;

    // About Exp
    private ObjectPool expOP;
    protected ObjectPool enemyOP = null;        // 상속받는 Enemy class에서 할당

    // Stat <- From Character

    public bool CanFly { get { return canFly; } }

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.Find("Player");
        expOP = GameObject.Find("Exp Object Pool").GetComponent<ObjectPool>();
    }

    protected virtual void Update()
    {
        EnableAttack();
        Move();
        Rotate();
    }

    protected virtual void Move()
    {
        if (isMove)
        {
            animator.SetBool("isMove", true);
            this.transform.position += dir.normalized * Time.deltaTime * _stat.Speed;
        }
    }

    protected void Rotate()
    {
        if (!canFly)
        {
            // 지상 유닛은 x 축이 회전되면 안됨
            dir.y = 0f;
        }
        
        if (isMove)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(dir.normalized), Time.deltaTime * 8f);
        }
    }

    protected void EnableAttack()
    {
        dir = player.transform.position - this.transform.position;

        if (dir.magnitude < attackRange)
        {
            canAttack = true;       // 가까이 있음
        }
        else
        {
            canAttack = false;
        }

        if (canAttack)
        {
            float angle = Vector3.Angle(dir, this.transform.forward);

            if (angle < 15)
            {
                // 전방 30도 이내에 적이 있는 경우에만 공격
                animator.SetBool("isMove", false);
                animator.SetTrigger("isAttack");        // MeleeEnemy는 공격 애니메이션 중 isMove = false
            }
        }
    }


    // 공격
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().GetDamage(_stat.Damage);
        }

        return;
    }

    // 피격
    public void GetDamage(int damage)
    {
        _stat.DecreaseHp(damage);

        Die();
    }

    private void Die()
    {
        if (_stat.Hp <= 0)
        {
            // 경험치 드랍
            GameObject go_exp = expOP.Get();
            go_exp.GetComponent<Experience>().SetExp(exp);
            go_exp.transform.position = this.transform.position + new Vector3(0, 1f, 0);    // offset

            // 적 처치시 체력회복
            int hpRecoveryPerKill = player.GetComponent<Player>().Stat.KillHpAbsorption;
            if (hpRecoveryPerKill > 0)
            {
                player.GetComponent<Player>().Stat.Heal(hpRecoveryPerKill);
            }

            if (enemyOP == null)
            {
                // 오브젝트 풀이 할당이 안된 경우 Destroy
                Destroy(this.gameObject);
            }
            else
            {
                enemyOP.Return(this.gameObject);
            }
        }
    }    
}
