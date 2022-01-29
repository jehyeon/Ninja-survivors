using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Stat _stat;
    public Stat Stat { get { return _stat; } }

    void Awake()
    {
        _stat = gameObject.AddComponent<Stat>();
        _stat.Hp = 100;
    }

    protected virtual void Update()
    {
        Die();
    }

    public void GetDamage(int damage)
    {
        Debug.Log(_stat.Hp);
        _stat.DecreaseHp(damage);
        Debug.Log(_stat.Hp);
    }

    protected void Die()
    {
        if (_stat.Hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
