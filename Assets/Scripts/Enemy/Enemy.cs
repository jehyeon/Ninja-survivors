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
    protected bool isDie;
    protected Vector3 dir;
    public Collider enemyCollider;
    private Rigidbody enemyRigidbody;

    // target
    protected Player player;
    private SpawnSystem spawnSystem;
    public SpawnSystem SpawnSystem { get { return spawnSystem; } set { spawnSystem = value; } }

    // About Exp
    private ObjectPool expOP;
    protected ObjectPool enemyOP = null;        // 상속받는 Enemy class에서 할당

    // Stat <- From Character

    public bool CanFly { get { return canFly; } }

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.Find("Player").GetComponent<Player>();
        expOP = GameObject.Find("Exp Object Pool").GetComponent<ObjectPool>();
        enemyCollider = GetComponent<Collider>();
        enemyRigidbody = GetComponent<Rigidbody>();
        isDie = false;
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
        Debug.Log(damage);
        _stat.DecreaseHp(damage);

        if (_stat.Hp <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("isHit");
        }
    }

    private void Die()
    {
        // 스폰 카운트 - 1
        spawnSystem.DecreaseCount();

        // Die animation
        animator.SetTrigger("isDie");
        isDie = true;
        isMove = false;
        enemyCollider.enabled = false;
        enemyRigidbody.useGravity = false;
        if (canFly)
        {
            // 공중에서 죽어도 떨어지도록
            enemyRigidbody.useGravity = true;
        }

        KillToHpRecovery();     // 플레이어 처치당 체력 회복
        DropExp();              // 경험치 드랍
        
        Invoke("GoUnderGround", 5f);    // 5초 뒤 바닥으로 사라짐
    }

    private void DropExp()
    {
        GameObject go_exp = expOP.Get();
        go_exp.GetComponent<Experience>().SetExp(exp);
        go_exp.transform.position = this.transform.position + new Vector3(0, 1f, 0);    // offset
    }

    private void KillToHpRecovery()
    {
        int hpRecoveryPerKill = player.Stat.KillHpAbsorption;
        if (hpRecoveryPerKill > 0)
        {
            player.GetComponent<Player>().Stat.Heal(hpRecoveryPerKill);
        }
    }

    private void GoUnderGround()
    {
        enemyRigidbody.useGravity = true;
        Invoke("ReturnOP", 2f);     // 2초 뒤 초기화 후 오브젝트 풀로 return
    }

    private void ReturnOP()
    {
        if (enemyOP == null)
        {
            // 오브젝트 풀이 할당이 안된 경우 Destroy
            Destroy(this.gameObject);
        }
        else
        {
            // 리셋 후 오브젝트 풀로 return
            Reset();
            enemyOP.Return(this.gameObject);
        }
    }

    private void Reset()
    {
        // 애니메이션 초기화
        animator.SetTrigger("Reset");

        // 설정 리셋
        isDie = false;
        enemyCollider.enabled = true;
        enemyRigidbody.useGravity = true;
        if (canFly)
        {
            // 공중 유닛은 중력 영향을 안 받음
            enemyRigidbody.useGravity = false;
        }
        this.Stat.HpReset();
    }
}
