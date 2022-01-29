using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    // 방어
    private int _hp;

    // 공격
    private float _damage;
    private float _attackRange;
    private float _attackSpeed;

    // 이동
    private float _speed;

    // 특수

    public int Hp { get { return _hp; } set { _hp = value; } }
    public void DecreaseHp(int decrease)
    {
        _hp -= decrease;
    }
}
