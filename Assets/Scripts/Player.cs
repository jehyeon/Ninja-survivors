using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private GameManager gameManager;

    public GameObject go_weapon;
    private Sword sword;

    private Animator animator;

    private Exp _exp;

    // temp for test
    public int Hp;
    public int HpRecovery;
    public float Defense;
    public float EvasionPercent;

    public float Speed;
    public float JumpPower;

    public int Damage;
    public int AttackHpAbsorption;
    public int KillHpAbsorption;
    public float CriticalPercent;
    public float DoublePercent;
    public float AttackSpeed;
    public int AttackRange;

    // Cool time;
    private float hpRecoveryCooltime;

    public Exp exp { get { return _exp; } }

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // temp
        go_weapon = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).Find("Weapon").gameObject;
        sword = go_weapon.GetComponent<Sword>();

        animator = GetComponent<Animator>();

        // Init
        hpRecoveryCooltime = 0f;
        _exp = new Exp();
        gameManager.UpdateHpBar();
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
            _stat.AttackHpAbsorption = AttackHpAbsorption;
            _stat.KillHpAbsorption = KillHpAbsorption;
            _stat.CriticalPercent = CriticalPercent;
            _stat.DoublePercent = DoublePercent;
            _stat.AttackSpeed = AttackSpeed;
            UpdataAttackSpeed();
            _stat.AttackRange = AttackRange;
            UpdateAttackRange();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            // temp
            gameManager.LevelUp();
        }

        // 자동 회복
        RecoverHp();

        // 공격 시 sword collider 활성화
        ActivateWeapon();
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
        gameManager.UpdateHpBar();
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
        gameManager.UpdateHpBar();
    }

    // 공격 중인지 확인
    private bool IsAttacking()
    {
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Attack1") ||
            animator.GetCurrentAnimatorStateInfo(1).IsName("Attack2") ||
            animator.GetCurrentAnimatorStateInfo(1).IsName("Attack3") )
        {
            return true;
        }

        return false;
    }

    // 무기 활성화, 비활성화
    private void ActivateWeapon()
    {
        if (IsAttacking())
        {
            sword.ActivateWeapon();
        }
        else
        {
            sword.DeActivateWeapon();
        }
    }

    protected override void Die()
    {
        if (_stat.Hp <= 0)
        {
            Debug.Log("플레이어 사망");
        }
    }

    public void GainExp(int amount)
    {
        bool result = this._exp.GainExp(amount);

        if (result)
        {
            gameManager.LevelUp();
        }

        gameManager.UpdateExpBar();
    }

    public void UpdataAttackSpeed()
    {
        animator.SetFloat("AttackSpeed", _stat.AttackSpeed);
    }

    public void UpdateAttackRange()
    {
        sword.UpgradeAttackRange(_stat.AttackRange);
    }
}
