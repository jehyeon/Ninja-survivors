using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalAbilityCommand : MonoBehaviour
{
    private Player player;
    private List<IntervalAbility> abilities;

    private void Awake()
    {
        player = GetComponent<Player>();
        abilities = new List<IntervalAbility>();
    }

    private bool LevelUpAbility(int abilityType)
    {
        IntervalAbility exist = null;

        switch (abilityType)
        {
            case 1:
                exist = player.GetComponent<Judgement>();
                break;
            case 2:
                exist = player.GetComponent<ReinforceWeapon>();
                break;
        }

        if (exist == null)
        {
            return false;
        }

        exist.LevelUp();
        return true;
    }

    private IntervalAbility CreateAbility(Ability ability)
    {
        switch(ability.SpecialAbilityId)
        {
            case 1:
                Judgement judgement = gameObject.AddComponent<Judgement>();
                judgement.GetPlayer(player);
                return judgement;
            case 2:
                ReinforceWeapon reinforceWeapon = gameObject.AddComponent<ReinforceWeapon>();
                reinforceWeapon.GetWeapon(player.sword);
                return reinforceWeapon;
        }

        // Bug
        return null;
    }

    public void AddAbility(Ability ability)
    {
        if (ability.Type != 1)
        {
            // Bug
            return;
        }

        Debug.LogFormat("{0} {1} {2}", ability.Id, ability.Type, ability.SpecialAbilityId);

        if (!LevelUpAbility(ability.Type))
        {
            // 같은 어빌리티가 이미 있으면 레벨 업, 없으면 새로 추가
            abilities.Add(CreateAbility(ability));
        }
    }

    private void Update()
    {
        Excute();
    }

    private void Excute()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].timer += Time.deltaTime;
            if (abilities[i].timer > abilities[i].cooltime)
            {
                abilities[i].Excute();
                abilities[i].timer = 0;
            }
        }
    }
}