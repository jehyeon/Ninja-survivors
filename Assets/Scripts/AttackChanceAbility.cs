using UnityEngine;

public abstract class IntervalAbility : MonoBehaviour
{
    protected int level;
    public float chance;

    public abstract void Excute();

    public void Attack()
    {
        if (Random.value < chance)
        {
            Excute();
        }
    }

    public void LevelUp()
    {
        level += 1;
    }
}