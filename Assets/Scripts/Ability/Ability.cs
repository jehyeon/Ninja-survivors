using UnityEngine;

public class Ability
{
    private int _id;
    private string _name;
    private string _description;
    private int _rank;              // 0: Common, 1: UnCommon, 2: Rare, 3: Unique
    private Sprite _sprite;
    private int _type;              // 0: stat, 1: interval
    private int _count;             // Level
    private int _maxCount;          // Max Level
    private int _specialAbilityId;

    public int Id { get { return _id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public int Rank { get { return _rank; } }
    public Sprite Sprite { get { return _sprite; } }
    public int Type { get { return _type; } }
    public int Count { get { return _count; } }
    public int MaxCount { get { return _maxCount; } }
    public int SpecialAbilityId { get { return _specialAbilityId; } }

    public Ability(int id, string name, string description, int rank, int imageId, int type, int maxCount, int specialAbilityId)
    {
        _id = id;
        _name = name;
        _description = description;
        _rank = rank;
        _sprite = Resources.Load<Sprite>("Abilities/" + imageId);
        _type = type;
        _count = 1;
        _maxCount = maxCount;
        _specialAbilityId = specialAbilityId;
    }
}