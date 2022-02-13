using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Enemy target;
    private ObjectPool missileOP;

    private float delay;
    private float speed;
    private int damage;
    private bool goToEnemy;
    private float distance;     // target이 죽은 경우 주변 enemy 재검색 범위
    private float timeOut;
    private float existTime;

    private void Awake()
    {
        missileOP = GameObject.Find("Missile Object Pool").GetComponent<ObjectPool>();

        goToEnemy = false;
        delay = 0f;
        speed = 20f;
        damage = 5;

        distance = 30f;
        existTime = 0f;     // 새로운 enemy를 찾는 시간
        timeOut = 5f;       // 5초 동안 enemy를 못 찾으면 사라짐
    }

    private void Update()
    {
        if (target == null || !target.enemyCollider.enabled)
        {
            // go to enemy
            FindAnotherEnemy();
        }

        if (existTime > timeOut)
        {
            missileOP.Return(this.gameObject);
        }

        if (delay < .7f)
        {
            delay += Time.deltaTime;
        }
        else
        {
            goToEnemy = true;
        }

        if (goToEnemy && target != null && target.enemyCollider.enabled)
        {
            Vector3 dir = target.transform.position + new Vector3(0, 1f, 0) - this.transform.position;
            this.transform.position += dir.normalized * Time.deltaTime * speed;
        }
        else
        {
            // 하늘로 올라감
            this.transform.Translate(new Vector3(0, 1f, 0) * speed * Time.deltaTime);
        }
    }

    private void SetTarget(GameObject enemy)
    {
        target = enemy.GetComponent<Enemy>();
    }

    private void FindAnotherEnemy()
    {
        Collider[] nearColliders = Physics.OverlapSphere(this.transform.position, distance, 1 << 3);

        if (nearColliders.Length == 0)
        {
            // 새로운 주변 enemy를 계속 찾음
            existTime += Time.deltaTime;
            return;
        }

        // 새로운 target 설정
        SetTarget(nearColliders[0].gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().GetDamage(damage);
            missileOP.Return(this.gameObject);
        }

        return;
    }
}
