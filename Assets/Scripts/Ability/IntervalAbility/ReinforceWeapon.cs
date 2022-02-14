using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforceWeapon : IntervalAbility
{
    private Sword weapon;

    private void Awake()
    {
        level = 1;
        timer = 0f;
        cooltime = 3f;
    }

    public override void Excute()
    {
        Debug.Log("실행");
        Debug.Log(weapon);
    }

    public void GetWeapon(Sword _weapon)
    {
        weapon = _weapon;
    }
}
