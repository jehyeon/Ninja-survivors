using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MeleeEnemy
{
    private void Start()
    {
        canFly = false;
        
        attackRange = 2f;
        attackCooltime = 0.667f;
        _stat.Hp = 30;
        _stat.Speed = 5;
        _stat.Damage = 5;

        exp = 50;
    }
}
