using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterFactory : AbsEnemyFactory
{
    private ObjectPool specterOP;

    private void Awake()
    {
        specterOP = GameObject.Find("Specter Object Pool").GetComponent<ObjectPool>();
    }

    public override Enemy CreateEnemy(string type)
    {   
        Enemy enemy = null;

        if (type.Equals("default"))
        {
            enemy = specterOP.Get().GetComponent<Specter>();
        }

        return enemy;
    }
}
