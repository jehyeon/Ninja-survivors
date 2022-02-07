public class Ability
{
    private int _id;
    private int _count;
    private string _name;
    private Sprite _sprite;

    public int Id { get { return _id; } }
    public int Count { get { return _count; } }
    public string Name { get { return _name; } }
    public Sprite Sprite { get { return _sprite; } }

    public Ability(int id, string name, int imageId)
    {
        _id = id;
        _count = 1;
        _name = name;
        _sprite = Resources.Load<Sprite>("Abilities/" + imageId);
    }
}