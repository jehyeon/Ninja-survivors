using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfFactory : AbsEnemyFactory
{
    private ObjectPool wolfOP;

    private void Awake()
    {
        wolfOP = GameObject.Find("Wolf Object Pool").GetComponent<ObjectPool>();
    }

    public override Enemy CreateEnemy(string type)
    {   
        Enemy enemy = null;

        if (type.Equals("default"))
        {
            enemy = wolfOP.Get().GetComponent<Wolf>();
        }

        return enemy;
    }
}
