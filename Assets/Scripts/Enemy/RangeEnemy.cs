using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    protected ObjectPool projectileOP;
    [SerializeField]
    protected GameObject projectileStartPos;

    protected float projectileSpeed;
    protected float attackDelay;

    protected override IEnumerator Attack()
    {
        yield return StartCoroutine(base.Attack());
        yield return new WaitForSeconds(attackDelay);        // attack start delay

        GameObject projectile = projectileOP.Get();
        projectile.transform.position = projectileStartPos.transform.position;
        projectile.GetComponent<EnemyProjectile>().Shoot(projectileOP, player.transform.position + new Vector3(0, 1f, 0), projectileSpeed, _stat.Damage);
    }
}
