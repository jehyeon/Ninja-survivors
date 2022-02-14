using UnityEngine;

public abstract class CriticalAttackAbility : MonoBehaviour
{
    protected int level;

    public abstract void Excute();

    public void LevelUp()
    {
        level += 1;
    }
}