using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
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

    private float time = 0f;

    // temp
    public float spawnCoolTime = 3f;
    public int enemyCountPerSpawn = 2;
    // Spawn 함수 정의해놓고 GameManager에서 일정 시간마다 스폰되도록 수정하기

    void Update()
    {
        time += Time.deltaTime;
        if (time > spawnCoolTime)
        {
            time = 0;
            for (int i = 0; i < enemyCountPerSpawn; i++)
            {
                Enemy enemy = ratFactory.CreateEnemy("default");
                SetRandomPosition(enemy);
            }
        }
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
}
