using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    // 방어
    private int _hp;
    private int _hpRecovery;
    private float _defense;
    private float _evasionPercent;

    // 공격
    private int _damage;
    private float _criticalPercent;
    private float _doublePercent;
    private float _attackSpeed;
    private int _attackRange;       // Level당 20% 씩 범위 증가

    // 이동
    private float _speed;
    private float _jumpPower;

    // 특수

    public int Hp { get { return _hp; } set { _hp = value; } }
    public int HpRecovery { get { return _hpRecovery; } set { _hpRecovery = value; } }
    public float Defense { get { return _defense; } set { _defense = value; } }
    public float EvasionPercent { get { return _evasionPercent; } set { _evasionPercent = value; } }

    // 공격
    public int Damage { get { return _damage; } set { _damage = value; } }
    public float CriticalPercent { get { return _criticalPercent; } set { _criticalPercent = value; } }
    public float DoublePercent { get { return _doublePercent; } set { _doublePercent = value; } }
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public int AttackRange { get { return _attackRange; } set { _attackRange = value; } }

    // 이동
    public float Speed { get { return _speed; } set { _speed = value; } }
    public float JumpPower { get { return _jumpPower; } set { _jumpPower = value; } }
    
    public void DecreaseHp(int decrease)
    {
        _hp -= decrease;
    }

    public void Heal(int amount)
    {
        _hp += amount;
    }
}
