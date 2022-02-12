using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabFactory : AbsEnemyFactory
{
    private ObjectPool crabOP;

    private void Awake()
    {
        crabOP = GameObject.Find("Crab Object Pool").GetComponent<ObjectPool>();
    }

    public override Enemy CreateEnemy(string type)
    {
        Enemy enemy = null;

        if (type.Equals("default"))
        {
            enemy = crabOP.Get().GetComponent<Crab>();
        }

        return enemy;
    }
}
