using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderFactory : AbsEnemyFactory
{
    private ObjectPool beholderOP;

    private void Awake()
    {
        beholderOP = GameObject.Find("Beholder Object Pool").GetComponent<ObjectPool>();
    }

    public override Enemy CreateEnemy(string type)
    {   
        Enemy enemy = null;

        if (type.Equals("default"))
        {
            enemy = beholderOP.Get().GetComponent<Beholder>();
        }

        return enemy;
    }
}
