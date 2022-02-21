using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public enum WeaponType
    {
        Katana,
        Bow,
        Shuriken
    }
    protected WeaponType weaponType;

    [SerializeField]
    protected GameObject go_player;
    protected Stat stat;
    protected AttackAbilityCommand attackAbilityCommand;
    
    public abstract void Attack(int attackType);
    public abstract void AttackOnAir(int attackType);
    public abstract void UpgradeAttackRange(int level = 1);
    public abstract void AttackSuccess(Collider collider);
}
