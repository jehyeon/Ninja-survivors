using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public GameObject go_weapon;

    private void Start()
    {
        go_weapon = transform.Find("Weapon").gameObject;
    }
}
