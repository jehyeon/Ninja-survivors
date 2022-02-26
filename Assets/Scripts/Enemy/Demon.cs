using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : RangeEnemy
{

    private void Start()
    {
        enemyOP = GameObject.Find("Demon Object Pool").GetComponent<ObjectPool>();
        projectileOP = GameObject.Find("Fire Object Pool").GetComponent<ObjectPool>();
        canFly = true;
        
        attackRange = 15f;
        projectileSpeed = 20f;
        attackCooltime = 3f;
        attackDelay = .5f;
        _stat.Hp = 20;
        _stat.Speed = 6;
        _stat.Damage = 30;

        exp = 50;
    }
}
