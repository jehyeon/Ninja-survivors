using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private GameObject go_player;
    private Stat stat;
    private BoxCollider _collider;

    private void Start()
    {
        InitCollider();
        GetPlayerObject();

        stat = go_player.GetComponent<Stat>();
    }

    private void InitCollider()
    {
        _collider = gameObject.AddComponent<BoxCollider>();
        _collider.isTrigger = true;
        _collider.enabled = false;
    }
    
    private void GetPlayerObject()
    {
        // parent * 11
        go_player = this.gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
    }

    public void ActivateWeapon()
    {
        // collider 활성화
        _collider.enabled = true;
    }

    public void DeActivateWeapon()
    {
        // collider 비활성화
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 더블공격
            if (Random.value < stat.DoublePercent)
            {
                Attack(other);
            }
            Attack(other);
        }

        return;
    }

    private void Attack(Collider other)
    {
        // 크리티컬
        if (Random.value < stat.CriticalPercent)
        {
            other.gameObject.GetComponent<Enemy>().GetDamage(stat.Damage * 2);
        }
        else
        {
            other.gameObject.GetComponent<Enemy>().GetDamage(stat.Damage);
        }
    }

    public void UpgradeAttackRange(int level = 1)
    {
        // Level당 20% 씩 범위 증가
        for (int i = 0; i < level; i++)
        {
            _collider.center += new Vector3(0, 0.1f, 0);
            _collider.size += new Vector3(0, 0.2f, 0);
        }
    }
}
