using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    // 원거리 유닛
    [SerializeField]
    protected GameObject go_enemyProjectile;   // 직접 넣어줘야 함

    protected override void Awake()
    {
        base.Awake();
    }
}
