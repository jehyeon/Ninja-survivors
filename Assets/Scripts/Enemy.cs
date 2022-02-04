using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private GameObject player;
    private ObjectPool expOP;
    private Animator animator;

    [SerializeField]
    private GameObject go_enemyWeapon;   // 직접 넣어줘야 함
    private BoxCollider enemyWeaponCollider;

    private bool isMove;
    private bool canAttack;     // 공격 범위 안에 있는지
    private float attackRange = 2f;    // temp
    private int exp = 10;   // temp

    void Start()
    {
        player = GameObject.Find("Player");
        expOP = GameObject.Find("Exp Object Pool").GetComponent<ObjectPool>();

        // Enemy table 만들기 전까지 임시
        _stat.Speed = 5;
        _stat.Damage = 5;
        _stat.Hp = 10;

        animator = GetComponent<Animator>();
        enemyWeaponCollider = go_enemyWeapon.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Act();
    }

    private void Act()
    {
        ActivateWeapon();

        Vector3 dir = player.transform.position - this.transform.position;
        if (dir.magnitude < attackRange)
        {
            isMove = false;
            animator.SetBool("isMove", false);
            animator.SetTrigger("isAttack");
        }
        else
        {
            isMove = true;
            animator.SetBool("isMove", true);
            this.transform.position += dir.normalized * Time.deltaTime * _stat.Speed;
            // 지상 유닛은 x 축이 회전되면 안됨
            dir.y = 0f;
            this.transform.rotation = Quaternion.LookRotation(dir.normalized);
        }
    }

    // 무기 collider 활성화
    private void ActivateWeapon()
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
        Debug.Log(_stat.Hp);
    }

    protected override void Die()
    {
        if (_stat.Hp <= 0)
        {
            // 경험치 드랍
            GameObject go_exp = expOP.Get();
            // 성능 저하가 생기면 오브젝트 풀 프리팹을 Experience로 수정
            go_exp.GetComponent<Experience>().SetExp(exp);
            go_exp.transform.position = this.transform.position + new Vector3(0, 1f, 0);

            Destroy(this.gameObject);      // temp
        }
    }    
}
