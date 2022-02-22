using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private GameManager gameManager;
    private WeaponSystem weaponSystem;
    private Weapon weapon;

    private Exp _exp;

    public Exp exp { get { return _exp; } }

    protected override void Awake()
    {
        base.Awake();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        weaponSystem = GetComponent<WeaponSystem>();
    }

    private void Start()
    {
        _exp = new Exp();
        _stat.Hp = 100;
        _stat.MaxHp = 100;
        _stat.JumpPower = 15;
        _stat.Damage = 5;
        _stat.Speed = 6;
        _stat.AttackSpeed = 1f;
        _stat.HpRecovery = 1;
        UpdataAttackSpeed();

        gameManager.UpdateHpBar();
        
        // 무기 데이터 가져오기
        weapon = weaponSystem.Weapon;

        InvokeRepeating("RecoverHp", 1f, 1f);   // 1초마다 체력회복
    }

    private void Update()
    {
        // for test
        if (Input.GetKeyDown(KeyCode.L))
        {
            gameManager.LevelUp();
        }
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
            // 0.95 -> 0, 1.9 -> 1
            _stat.DecreaseHp(Mathf.FloorToInt((float)damage * (float)(100 - _stat.Defense) / 100f));
        }
        gameManager.UpdateHpBar();

        Die();      // 피격 이후 체력이 0 이하면 Game Over
    }

    private void RecoverHp()
    {
        // 1초마다 체력 회복

        if (_stat.HpRecovery == 0)
        {
            return;
        }

        if (_stat.Heal(_stat.HpRecovery))
        {
            gameManager.UpdateHpBar();
        }
    }

    private void Die()
    {
        if (_stat.Hp <= 0)
        {
            gameManager.GameOver();
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
        weaponSystem.Weapon.UpdateAttackCooltime();
        animator.SetFloat("attackSpeed", _stat.AttackSpeed);
    }

    public void UpdateAttackRange()
    {
        weapon.UpgradeAttackRange(_stat.AttackRange);
    }
}
