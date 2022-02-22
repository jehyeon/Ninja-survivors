using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MeleeEnemy
{
    private void Start()
    {
        enemyOP = GameObject.Find("Crab Object Pool").GetComponent<ObjectPool>();
        canFly = false;
        
        attackRange = 3f;
        attackCooltime = 1.2f;
        _stat.Hp = 5;
        _stat.Speed = 3;
        _stat.Damage = 5;

        exp = 10;
    }
}