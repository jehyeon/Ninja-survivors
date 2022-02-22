using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MeleeEnemy
{
    private void Start()
    {
        canFly = false;
        
        attackRange = 2.5f;
        attackCooltime = 1.067f;
        _stat.Hp = 50;
        _stat.Speed = 5;
        _stat.Damage = 10;

        exp = 100;
    }
}

