using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private GameObject go_player;
    private Stat stat;

    private int _damage = 5;

    private void Start()
    {
        InitCollider();
        GetPlayerObject();

        stat = go_player.GetComponent<Stat>();
    }

    private void InitCollider()
    {
        Collider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
    }
    
    private void GetPlayerObject()
    {
        // parent * 11
        go_player = this.gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
    }

    void ActivateWeapon()
    {
        // collider 활성화
    }

    void DeActivateWeapon()
    {
        // collider 비활성화
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
            other.gameObject.GetComponent<Enemy>().GetDamage(_damage * 2);
        }
        else
        {
            other.gameObject.GetComponent<Enemy>().GetDamage(_damage);
        }
    }
}
