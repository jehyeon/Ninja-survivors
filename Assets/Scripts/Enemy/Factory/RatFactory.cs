using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatFactory : AbsEnemyFactory
{
    private ObjectPool ratOP;

    private void Awake()
    {
        ratOP = GameObject.Find("Rat Object Pool").GetComponent<ObjectPool>();
    }

    public override Enemy CreateEnemy(string type)
    {   
        Enemy enemy = null;

        if (type.Equals("default"))
        {
            enemy = ratOP.Get().GetComponent<Rat>();
        }

        return enemy;
    }
}
