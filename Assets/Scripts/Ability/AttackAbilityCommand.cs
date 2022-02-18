using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbilityCommand : MonoBehaviour
{
    private Player player;
    private List<AttackChanceAbility> attackChanceAbilities;
    private List<AttackCountAbility> attackCountAbilities;
    private List<CriticalAttackAbility> criticalAttackAbilities;

    private void Awake()
    {
        attackChanceAbilities = new List<AttackChanceAbility>();
        attackCountAbilities = new List<AttackCountAbility>();
        criticalAttackAbilities = new List<CriticalAttackAbility>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Attack()
    {
        // Excute 실행 조건이 달라서 for 문을 나눔
        for (int i = 0; i < attackChanceAbilities.Count; i++)
        {
            attackChanceAbilities[i].Attack();

        }

        for (int i = 0; i < attackCountAbilities.Count; i++)
        {
            attackCountAbilities[i].Attack();
        }
    }

    public void CriticalAttack()
    {
        for (int i = 0; i < criticalAttackAbilities.Count; i++)
        {
            criticalAttackAbilities[i].Excute();
        }
    }

    public void AddAbility(Ability ability)
    {
        switch(ability.Type)
        {
            case 2:
                // Attack chance
                // -> !!! check exist
                AddAttackChanceAbility(ability);
                break;
            case 3:
                // Attack count
                // -> !!! check exist
                AddAttackCountAbility(ability);
                break;
            case 4:
                // Critical attack
                // -> !!! check exist
                AddCriticalAttackAbility(ability);
                break;
        }
    }

    private void AddAttackChanceAbility(Ability ability)
    {
        switch(ability.SpecialAbilityId)
        {
            case 1:
                MissileAttack missileAttack = this.gameObject.AddComponent<MissileAttack>();
                missileAttack.SetPlayer(player);
                attackChanceAbilities.Add(missileAttack);
                break;
        }
    }

    private void AddAttackCountAbility(Ability ability)
    {
        switch(ability.SpecialAbilityId)
        {
            case 1:
                break;
        }
    }

    private void AddCriticalAttackAbility(Ability ability)
    {
        switch(ability.SpecialAbilityId)
        {
            case 1:
                break;
        }
    }

    // !!! level up
}