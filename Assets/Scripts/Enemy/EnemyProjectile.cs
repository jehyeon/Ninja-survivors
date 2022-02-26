using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector3 dir;    // 방향벡터
    private int damage;
    private float speed;
    private ObjectPool projectileOP;

    public void Shoot(ObjectPool objectPool, Vector3 playerPos, float projectileSpeed, int statDamage)
    {
        projectileOP = objectPool;
        dir = playerPos - this.transform.position;
        speed = projectileSpeed;
        damage = statDamage;

        InvokeRepeating("Move", 0f, 0.0016f);
        Invoke("Return", 5f);   // 5초 뒤 사라짐
    }

    private void Move()
    {
        this.transform.Translate(dir.normalized * speed * 0.0016f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().GetDamage(damage);
            Return();
        }
        
        if (other.gameObject.CompareTag("Ground"))
        {
            // 지면에 닿으면 사라짐
            Return();
        }

        return;
    }
    
    private void Return()
    {
        projectileOP.Return(this.gameObject);
    } 
}
