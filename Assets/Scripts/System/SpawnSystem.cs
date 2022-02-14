using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float minZ;
    [SerializeField]
    private float maxZ;
    [SerializeField]
    private float flyEnemyMinY;
    [SerializeField]
    private float flyEnemyMaxY;
    [SerializeField]
    private RatFactory ratFactory;
    [SerializeField]
    private CrabFactory crabFactory;
    [SerializeField]
    private BlackNightFactory blackNightFactory;
    [SerializeField]
    private SpecterFactory specterFactory;

    private float time = 0f;
    public int enemyCount = 0;
    private int minEnemyCount = 20;
    private int maxEnemyCount = 100;
    private int spawnLevel = 0;

    // temp
    private float spawnCoolTime = 1f;
    private int enemyCountPerSpawn = 5;
    // Spawn 함수 정의해놓고 GameManager에서 일정 시간마다 스폰되도록 수정하기

    private string[][] spawnTable = new string[15][];

    private void Start()
    {
        // 스폰 테이블
        spawnTable[0] = new string[] {"Crab"};
        spawnTable[1] = new string[] {"Rat"};
        spawnTable[2] = new string[] {"Rat", "Crab"};
        spawnTable[3] = new string[] {"Specter"};
        spawnTable[4] = new string[] {"Rat", "Crab", "Specter"};
        // 게임 시작 5초 이후 spawnCoolTime마다 스폰
        InvokeRepeating("Spawn", 2f, spawnCoolTime);
        InvokeRepeating("BigSpawn", 600f, 600f);
        InvokeRepeating("UpdateSpawnTable", 120f, 120f);
    }

    private void SetRandomPosition(Enemy enemy)
    {
        // 랜덤 좌표로 위치 조정
        enemy.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(flyEnemyMinY, flyEnemyMaxY), Random.Range(minZ, maxZ));
        
        if (!enemy.CanFly)
        {
            RaycastHit hit;

            if (Physics.Raycast(enemy.transform.position, new Vector3(0, -1, 0), out hit, 50f, 1 << 6))
            {
                enemy.transform.position = new Vector3(enemy.transform.position.x, hit.point.y, enemy.transform.position.z);
            }
        }
    }

    private void BigSpawn()
    {
        // 10분 간격으로 Big wave
    }

    private void Spawn()
    {
        if (enemyCount > maxEnemyCount)
        {
            // 몹 생성 제한
            return;
        }

        enemyCount += 1;

        // Enemy select
        string enemyType = spawnTable[spawnLevel][Random.Range(0, spawnTable[spawnLevel].Length)];
        Enemy enemy = null;
        switch (enemyType)
        {
            case "Rat":
                enemy = ratFactory.CreateEnemy("default");
                break;
            case "Crab":
                enemy = crabFactory.CreateEnemy("default");
                break;
            case "Specter":
                enemy = specterFactory.CreateEnemy("default");
                break;
            case "BlackNight":
                enemy = blackNightFactory.CreateEnemy("default");
                break;
        }
        
        SetRandomPosition(enemy);

        if (enemy.SpawnSystem == null)
        {
            enemy.SpawnSystem = GetComponent<SpawnSystem>();
        }

        if (enemyCount < minEnemyCount)
        {
            // 몹 재생성
            Spawn();
        }
    }

    private void UpdateSpawnTable()
    {
        spawnLevel += 1;
    }

    public void DecreaseCount()
    {
        enemyCount -= 1;
    }
}
