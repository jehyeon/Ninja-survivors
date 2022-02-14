using UnityEngine;

public abstract class IntervalAbility : MonoBehaviour
{
    protected int level;
    public float timer;
    public float cooltime;

    public abstract void Excute();

    public void LevelUp()
    {
        level += 1;
    }
}