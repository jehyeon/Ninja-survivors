using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonFactory : AbsEnemyFactory
{
    private ObjectPool demonOP;

    private void Awake()
    {
        demonOP = GameObject.Find("Demon Object Pool").GetComponent<ObjectPool>();
    }

    public override Enemy CreateEnemy(string type)
    {   
        Enemy enemy = null;

        if (type.Equals("default"))
        {
            enemy = demonOP.Get().GetComponent<Demon>();
        }

        return enemy;
    }
}
