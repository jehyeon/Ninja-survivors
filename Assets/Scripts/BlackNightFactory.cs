using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackNightFactory : AbsEnemyFactory
{
    private ObjectPool blackNightOP;

    private void Awake()
    {
        blackNightOP = GameObject.Find("BlackNight Object Pool").GetComponent<ObjectPool>();
    }

    public override Enemy CreateEnemy(string type)
    {   
        Enemy enemy = null;

        if (type.Equals("default"))
        {
            enemy = blackNightOP.Get().GetComponent<BlackNight>();
        }

        return enemy;
    }
}
