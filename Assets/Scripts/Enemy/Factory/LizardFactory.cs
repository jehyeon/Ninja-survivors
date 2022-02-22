using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardFactory : AbsEnemyFactory
{
    private ObjectPool lizardOP;

    private void Awake()
    {
        lizardOP = GameObject.Find("Lizard Object Pool").GetComponent<ObjectPool>();
    }

    public override Enemy CreateEnemy(string type)
    {   
        Enemy enemy = null;

        if (type.Equals("default"))
        {
            enemy = lizardOP.Get().GetComponent<Lizard>();
        }

        return enemy;
    }
}
