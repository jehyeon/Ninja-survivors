using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private RatFactory ratFactory;

    void Start()
    {
        Enemy enemy = ratFactory.CreateEnemy("default");
    }
}
