using System.Collections.Generic;
public class AbilityManager {
    private List<Dictionary<string, object>> data;
    private int _count;
    public int Count { get { return _count; } }
    public AbilityManager()
    {
        data = CSVReader.Read("CSV/Abilities");
        _count = data.Count;
    }

    public Ability Get(int abilityId)
    {
        Ability ability = new Ability(
            abilityId,
            abilityManager.data[abilityId]["name"].ToString(),
            (int)abilityManager.data[abilityId]["imageId"]
        );

        return ability;
    }

    public Ability GetRandomAbility(int numOfTry = 3)
    {
        // 서로 다른 abilitiy 3개
        // rank별로 확률 존재, maxCount인 ability는 예외 처리

        // !!! (임시) 전체 중 아무거나 3개 중복가능, 수 제한 없음
        List<Ability> abilities = new List<Ability>();

        for (int i = 0; i < numOfTry; i++)
        {
            abilities.Add(Get(Random.Range(0, _count)));
        }

        return abilities;
    }
}