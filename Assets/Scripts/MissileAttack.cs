using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttack : AttackChanceAbility
{
    private Player player;
    private ObjectPool missileOP;

    private void Awake()
    {
        chance = 1f;
    }

    private void Start()
    {
        missileOP = GameObject.Find("Missile Object Pool").GetComponent<ObjectPool>();
    }

    public override void Excute()
    {
        Shoot();
    }

    private void Shoot()
    {
        GameObject missile = missileOP.Get();
        missile.transform.position = player.transform.position + new Vector3(0f, 1f, 0f);
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
    }
}
