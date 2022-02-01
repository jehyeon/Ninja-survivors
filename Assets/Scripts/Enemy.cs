using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void GetDamage(int damage)
    {
        _stat.DecreaseHp(damage);
        Debug.Log(_stat.Hp);
    }    
}