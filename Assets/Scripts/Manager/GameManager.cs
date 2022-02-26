using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;
    private UI ui;
    private AbilityManager abilityManager;
    private IntervalAbilityCommand intervalAbilityCommand;
    private AttackAbilityCommand attackAbilityCommand;
    private int playTime;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        intervalAbilityCommand = player.GetComponent<IntervalAbilityCommand>();
        attackAbilityCommand = player.GetComponent<AttackAbilityCommand>();
        ui = GameObject.Find("Canvas").GetComponent<UI>();

        abilityManager = new AbilityManager();

        UnPause();
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 고정

        // 플레이 타임
        playTime = 0;
        InvokeRepeating("UpdatePlayTime", 0f, 1f);
    }

    private void Start()
    {
        // 시작 옵션 (load로 수정 예정)
        // !!! temp
        ui.SetMouseSensitivity(10f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ui.OptionMode)
            {
                CloseOption();
            }
            else
            {
                OpenOption();
            }
        }
    }

    //////////////////////////////////////////////////////////
    // Game 진행 관련 --------------------------------------- //
    //////////////////////////////////////////////////////////
    private void Pause()
    {
        // !!! temp
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None; // 마우스 고정해제
    }

    private void UnPause()
    {
        // !!! temp
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;   // 마우스 고정
    }

    private void UpdatePlayTime()
    {
        ui.UpdatePlayTime(playTime);
        playTime += 1;
    }

    // Player
    public void LevelUp()
    {
        FillAbility();
    }

    public void GameOver()
    {
        Pause();
        ui.OpenGameOverUI();
    }

    //////////////////////////////////////////////////////////
    // UI ------------------------------------------------- //
    //////////////////////////////////////////////////////////
    public void UpdateHpBar()
    {
        ui.UpdateHpBar(player.Stat.Hp, player.Stat.MaxHp);
    }

    public void UpdateExpBar()
    {
        ui.UpdateExpBar(player.exp.Now);
    }

    //////////////////////////////////////////////////////////
    // Ability -------------------------------------------- //
    //////////////////////////////////////////////////////////
    private void FillAbility()
    {
        // Ability pop up 채우기
        ui.OpenAbilityPopups();
        ui.FillAbilityPopups(abilityManager.GetRandomAbility());
        Pause();    // 일시 정지
    }

    public void SelectAbility(Ability ability)
    {
        // Ability pop up에서 선택
        ui.CloseAbilityPopups();

        // abilityManager에서 남은 어빌리티 잔여 수 확인
        abilityManager.DecreaseAbilityMaxCount(ability.Id);
        
        switch (ability.Type)
        {
            case 0:
                // Stat 적용
                AddStatAbility(ability.Id);
                break;
            case 1:
                AddIntervalAbility(ability);
                break;
            case 2:
            case 3:
            case 4:
                // attack chance, attack count, critical attack
                // AttackAbilityCommand에서 처리
                AddAttackAbility(ability);
                break;
        }

        // 선택한 ability를 UI에 추가
        ui.AddAbilitySlot(ability);

        // !!! 어빌리티 합성 시스템 추가

        UnPause();  // 일시정지 해제
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
            player.Stat.AttackSpeed += (float)attackSpeed / 100f;
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

    private void AddIntervalAbility(Ability ability)
    {
        intervalAbilityCommand.AddAbility(ability);
    }

    private void AddAttackAbility(Ability ability)
    {
        attackAbilityCommand.AddAbility(ability);
    }


    //////////////////////////////////////////////////////////
    // Settings ------------------------------------------- //
    //////////////////////////////////////////////////////////
    private void OpenOption()
    {
        // 옵션 창 열고 게임 정지
        ui.OpenOptionUI();
        Pause();
    }

    public void CloseOption()
    {
        ui.CloseOptionUI();
        UnPause();
    }
}
