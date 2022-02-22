using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specter : MeleeEnemy
{
    protected override void Awake()
    {
        base.Awake();

        canFly = true;
    }

    private void Start()
    {
        enemyOP = GameObject.Find("Specter Object Pool").GetComponent<ObjectPool>();

        attackRange = 2f;
        attackCooltime = 0.733f;

        _stat.Hp = 5;
        _stat.Speed = 6;
        _stat.Damage = 3;

        exp = 10;
    }
}
