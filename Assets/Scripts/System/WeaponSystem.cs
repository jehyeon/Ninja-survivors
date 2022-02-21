using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject katana; 
    private Weapon.WeaponType selectedWeaponType;
    private Weapon weapon;
    public Weapon Weapon {
        get {
            return weapon;
        }
    }

    void Start()
    {
        // temp
        selectedWeaponType = Weapon.WeaponType.Katana;
        InitWeapon();
    }

    private void InitWeapon()
    {
        // Deactivate all weapon
        katana.SetActive(false);

        switch(selectedWeaponType)
        {
            case Weapon.WeaponType.Katana:
                weapon = katana.GetComponent<Katana>();
                break;
            case Weapon.WeaponType.Bow:
                break;
            case Weapon.WeaponType.Shuriken:
                break;
        }

        weapon.gameObject.SetActive(true);
    }

    public void Attack(int attackType)
    {
        weapon.Attack(attackType);
    }

    public void AttackOnAir(int attackType)
    {
        weapon.AttackOnAir(attackType);
    }
}
