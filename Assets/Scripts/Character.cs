using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Stat _stat;
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

    protected virtual void Die()
    {
        if (_stat.Hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
