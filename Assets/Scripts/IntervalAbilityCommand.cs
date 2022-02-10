public class IntervalAbilityCommand
{
    private Player player;
    private List<IntervalAbility> abilities;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void AddAbility(Ability ability)
    {
        Debug.LogFormat("{0} {1} {2}", ability.Id, ability.Type, ability.SpecialAbilityId);
    }

    private void Update()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].timer += Time.deltatime;
            if (abilities[i].timer > abilities[i].cooltime)
            {
                abilities[i].Excute();
                abilities[i].timer = 0;
            }
        }
    }
}