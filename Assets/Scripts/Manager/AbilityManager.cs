using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager {
    public List<Dictionary<string, object>> data;

    private List<int> commonAbilityIds;
    private List<int> uncommonAbilityIds;
    private List<int> rareAbilityIds;

    public AbilityManager()
    {
        data = CSVReader.Read("CSV/Abilities");
        commonAbilityIds = new List<int>();
        uncommonAbilityIds = new List<int>();
        rareAbilityIds = new List<int>();
        SeperateAbility();  // data -> Common, Uncommon, Rare
    }

    public List<Ability> GetAbilityByType(string abilityRank, int abilityCount)
    {
        if (abilityCount == 0)
        {
            return new List<Ability>();
        }

        // rank 선택
        List<int> selected = null;
        switch (abilityRank)
        {
            case "Rare":
                selected = rareAbilityIds;
                break;
            case "Uncommon":
                selected = uncommonAbilityIds;
                break;
            case "Common":
                selected = commonAbilityIds;
                break;
        }

        // 선택된 어빌리티 리스트 셔플 후 abilityCount만큼 추출 (중복 X)
        selected = Shuffle(selected);
        List<Ability> abilities = new List<Ability>();
        foreach (int abilityId in selected.GetRange(0, abilityCount))
        {
            abilities.Add(new Ability(
                abilityId,
                data[abilityId]["name"].ToString(),
                data[abilityId]["description"].ToString(),
                (int)data[abilityId]["rank"],
                (int)data[abilityId]["imageId"],
                (int)data[abilityId]["type"],
                (int)data[abilityId]["maxCount"],
                (int)data[abilityId]["specialAbilityId"]
            ));
        }

        return abilities;
    }

    public List<Ability> GetRandomAbility(int numOfTry = 3)
    {
        // rank별로 확률 존재
        // 66% -> 30.66% -> 0.3066
        // 15% -> 5.27%  -> 0.0527
        int rareCount = 0;
        int uncommonCount = 0;
        int commonCount = 0;

        for (int i = 0; i < numOfTry; i++)
        {
            float randomValue = Mathf.Round(UnityEngine.Random.value * 1000f) * 0.001f;
            if (randomValue <= 0.0527)
            {
                rareCount += 1;
            }
            else if (randomValue <= 0.3593)
            {
                uncommonCount += 1;
            }
            else 
            {
                commonCount += 1;
            }
        }

        List<Ability> abilities = new List<Ability>();
        
        abilities.AddRange(GetAbilityByType("Rare", rareCount));
        abilities.AddRange(GetAbilityByType("Uncommon", uncommonCount));
        abilities.AddRange(GetAbilityByType("Common", commonCount));

        return Shuffle(abilities);
    }

    private void SeperateAbility()
    {
        // ability rank에 맞게 분류
        if (data == null)
        {
            data = CSVReader.Read("CSV/Abilities");
        }

        for (int i = 0; i < data.Count; i++)
        {
            switch((int)data[i]["rank"])
            {
                case 0:
                    // Common
                    commonAbilityIds.Add(i);
                    break;
                case 1:
                    // Uncommon
                    uncommonAbilityIds.Add(i);
                    break;
                case 2:
                    // Rare
                    rareAbilityIds.Add(i);
                    break;
            }
        }
    }

    private List<T> Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int newIndex = UnityEngine.Random.Range(0, list.Count);
            T temp = list[i];
            list[i] = list[newIndex];
            list[newIndex] = temp;
        }

        return list;
    }

    public void DecreaseAbilityMaxCount(int abilityId)
    {
        data[abilityId]["maxCount"] = (int)data[abilityId]["maxCount"] - 1;

        if ((int)data[abilityId]["maxCount"] <= 0)
        {
            // 최대 레벨 달성
            // 어빌리티 목록에서 삭제 -> 더 이상 안 나옴
            data.RemoveAt(abilityId);
            switch((int)data[abilityId]["rank"])
            {
                case 0:
                    // Common
                    commonAbilityIds.Remove(abilityId);
                    break;
                case 1:
                    // Uncommon
                    uncommonAbilityIds.Remove(abilityId);
                    break;
                case 2:
                    // Rare
                    rareAbilityIds.Remove(abilityId);
                    break;
            }
        }
    }
}