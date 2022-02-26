using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder : RangeEnemy
{
    private void Start()
    {
        enemyOP = GameObject.Find("Beholder Object Pool").GetComponent<ObjectPool>();
        projectileOP = GameObject.Find("Lazer Object Pool").GetComponent<ObjectPool>();
        canFly = true;
        
        attackRange = 15f;
        projectileSpeed = 40f;
        attackCooltime = 2f;
        attackDelay = .5f;
        _stat.Hp = 20;
        _stat.Speed = 6;
        _stat.Damage = 1;

        exp = 25;
    }
}

