using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public GameObject go_weapon;
    private Animator animator;

    private float exp;

    // temp for test
    public int Hp;
    public int HpRecovery;
    public float Defense;
    public float EvasionPercent;

    public float Speed;
    public float JumpPower;

    public int Damage;
    public float CriticalPercent;
    public float DoublePercent;
    public float AttackSpeed;
    public int AttackRange;

    // Cool time;
    private float hpRecoveryCooltime;

    private void Start()
    {
        // temp
        go_weapon = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).Find("Weapon").gameObject;

        // Init
        hpRecoveryCooltime = 0f;

        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        // temp
        if (Input.GetKeyDown(KeyCode.P))
        {
            _stat.Hp = Hp;
            _stat.HpRecovery = HpRecovery;
            _stat.Defense = Defense;
            _stat.EvasionPercent = EvasionPercent;

            _stat.Speed = Speed;
            _stat.JumpPower = JumpPower;

            _stat.Damage = Damage;
            _stat.CriticalPercent = CriticalPercent;
            _stat.DoublePercent = DoublePercent;
            _stat.AttackSpeed = AttackSpeed;
            animator.SetFloat("AttackSpeed", _stat.AttackSpeed);
            _stat.AttackRange = AttackRange;
            go_weapon.GetComponent<Sword>().UpgradeAttackRange(_stat.AttackRange);        
        }

        RecoverHp();
    }

    // About Stat
    public void GetDamage(int damage)
    {
        if (Random.value < _stat.EvasionPercent)
        {
            // !!! 회피
            Debug.LogFormat("회피!");
            return;
        }

        if (_stat.Defense == 0)
        {
            _stat.DecreaseHp(damage);
        }
        else
        {
            _stat.DecreaseHp(Mathf.FloorToInt(damage / (1 - _stat.Defense)));
            Debug.LogFormat("defense: {0}", _stat.Defense);
        }

        Debug.Log(_stat.Hp);
    }

    private void RecoverHp()
    {
        if (_stat.HpRecovery == 0)
        {
            return;
        }

        hpRecoveryCooltime += Time.deltaTime;

        if (hpRecoveryCooltime > 1f)
        {
            // 1초마다 체력 회복
            _stat.Heal(_stat.HpRecovery);
            hpRecoveryCooltime = 0;
        }
    }
}
