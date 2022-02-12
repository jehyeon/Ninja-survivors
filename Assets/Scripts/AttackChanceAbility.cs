using UnityEngine;

public abstract class AttackChanceAbility : MonoBehaviour
{
    protected int level;
    protected float chance;

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