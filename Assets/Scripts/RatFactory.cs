using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatFactory : AbsEnemyFactory
{
    [SerializeField]
    private GameObject pref_rat;

    public override Enemy CreateEnemy(string type)
    {   
        Enemy enemy = null;

        if (type.Equals("default"))
        {
            enemy = Object.Instantiate(pref_rat).GetComponent<Rat>();
        }

        return enemy;
    }
}
