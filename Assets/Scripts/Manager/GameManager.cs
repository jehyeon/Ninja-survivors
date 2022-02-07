using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;
    private UI ui;
    private AbilityManager abilityManager;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        ui = GameObject.Find("Canvas").GetComponent<UI>();

        abilityManager = new AbilityManager();
    }

    public void LevelUp()
    {
        FillAbility();
    }

    // UI
    public void UpdateHpBar()
    {
        ui.UpdateHpBar(player.Stat.Hp, player.Stat.MaxHp);
    }

    public void UpdateExpBar()
    {
        ui.UpdateExpBar(player.exp.Now);
    }

    // Ability
    private void FillAbility()
    {
        // Ability pop up 채우기
        ui.OpenAbilityPopups();
        ui.FillAbilityPopups(abilityManager.GetRandomAbility());
    }

    public void SelectAbility(int abilityId)
    {
        // Ability pop up에서 선택
        ui.CloseAbilityPopups();

        switch ((int)abilityManager.data[abilityId]["type"])
        {
            case 0:
                // Stat 적용
                AddStatAbility(abilityId);
                break;
            default:
                // 선택한 ability를 UI에 추가
                ui.AddAbilitySlot(abilityId);
        }
    }

    // Stat 어빌리티
    private void AddStatAbility(int abilityId)
    {
        // Stat에 바로 적용
        // 최대 체력
        int maxHp = (int)abilityManager.data[abilityId]["hp"];
        if (maxHp != 0)
        {
            player.Stat.Hp += (int)abilityManager.data[abilityId]["hp"];
            player.Stat.MaxHp += (int)abilityManager.data[abilityId]["hp"];
            UpdateHpBar();
        }
        player.Stat.HpRecovery += (int)abilityManager.data[abilityId]["hpRecovery"];
        player.Stat.Defense += (int)abilityManager.data[abilityId]["defense"];
        player.Stat.EvasionPercent += (float)(int)abilityManager.data[abilityId]["evasionPercent"] / 100f;
        player.Stat.Damage += (int)abilityManager.data[abilityId]["damage"];
        player.Stat.AttackHpAbsorption += (int)abilityManager.data[abilityId]["attackHpAbsorption"];
        player.Stat.KillHpAbsorption += (int)abilityManager.data[abilityId]["killHpAbsorption"];
        player.Stat.CriticalPercent += (float)(int)abilityManager.data[abilityId]["criticalPercent"] / 100f;
        player.Stat.DoublePercent += (float)(int)abilityManager.data[abilityId]["doublePercent"] / 100f;

        // 공격속도
        int attackSpeed = (int)abilityManager.data[abilityId]["attackSpeed"];
        int attackRange = (int)abilityManager.data[abilityId]["attackRange"];
        if (attackSpeed != 0)
        {
            Debug.Log(player.Stat.AttackSpeed);
            Debug.Log(attackSpeed / 100f);
            player.Stat.AttackSpeed += (float)attackSpeed / 100f;
            Debug.Log(player.Stat.AttackSpeed);
            player.UpdataAttackSpeed();
        }
        if (attackRange != 0)
        {
            player.Stat.AttackRange += attackRange;
            player.UpdateAttackRange();
        }

        // 이동속도, 점프력 (PlayerController)
        player.Stat.Speed += (float)(int)abilityManager.data[abilityId]["speed"];
        player.Stat.JumpPower += (float)(int)abilityManager.data[abilityId]["jumpPower"];
    }
}