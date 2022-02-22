using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MeleeEnemy
{
    private void Start()
    {
        enemyOP = GameObject.Find("Rat Object Pool").GetComponent<ObjectPool>();
        canFly = false;
        
        attackRange = 2f;
        attackCooltime = 0.767f;
        _stat.Hp = 5;
        _stat.Speed = 6;
        _stat.Damage = 3;

        exp = 10;
    }
}
