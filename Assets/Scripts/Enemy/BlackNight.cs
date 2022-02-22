using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackNight : MeleeEnemy
{
    private void Start()
    {
        canFly = false;
        
        attackRange = 3f;
        attackCooltime = 1.5f;

        _stat.Hp = 50;
        _stat.Speed = 3;
        _stat.Damage = 50;

        exp = 100;
    }
}
