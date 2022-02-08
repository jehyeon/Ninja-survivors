using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private int spawnAreaCountPerLine;      // -count/2 ~ count/2 (한 줄에 count + 1 만큼 스폰 pos)
    [SerializeField]
    private float spawnAreaSize;

    [SerializeField]
    private RatFactory ratFactory;

    // temp
    private float spawnTime = 0f;
    
    // Spawn 함수 정의해놓고 GameManager에서 일정 시간마다 스폰되도록 수정하기

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

    private Pos GetRandomPosition()
    {
        if (spawnAreaCountPerLine % 2 == 1)
        {
            spawnAreaCountPerLine += 1;
        }

        int half = spawnAreaCountPerLine / 2;

        Pos spawnPos = new Pos(Random.Range(-1 * half, half), Random.Range(-1 * half, half));

        return spawnPos;
    }
}

public struct Pos
{
    public int x;
    public int y;

    public Pos(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}