using UnityEngine;

public abstract class AttackCountAbility : MonoBehaviour
{
    protected int level;
    public float attackCount;
    public float maxCount;

    public abstract void Excute();

    public void Attack()
    {
        attackCount += 1;
        if (attackCount >= maxCount)
        {
            Excute();
            attackCount = 0;
        }
    }

    public void LevelUp()
    {
        level += 1;
    }
}