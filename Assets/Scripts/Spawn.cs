using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private RatFactory ratFactory;

    // temp
    private float spawnTime = 0f;
        
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime > 4f)
        {
            spawnTime = 0;
            ratFactory.CreateEnemy("default");
            ratFactory.CreateEnemy("default");
        }
    }
}
