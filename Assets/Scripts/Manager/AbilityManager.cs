using System.Collections.Generic;
public class AbilityManager {
    public List<Dictionary<string, object>> data;
    private int _count;

    public int Count { get { return _count; } }
    public AbilityManager()
    {
        data = CSVReader.Read("CSV/Abilities");
        _count = data.Count;
    }
}