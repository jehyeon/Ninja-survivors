using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    private int _exp;
    private static float speed = 15f;

    private bool goToPlayer;
    private GameObject player;
    private ObjectPool expOP;
    
    private float delay;
    

    void Awake()
    {
        player = GameObject.Find("Player");
        expOP = GameObject.Find("Exp Object Pool").GetComponent<ObjectPool>();
        goToPlayer = false;
        delay = 0;
        _exp = 0;
    }

    void Update()
    {
        GoToPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().exp.GainExp(_exp);
            expOP.Return(this.gameObject);
        }

        return;
    }

    private void GoToPlayer()
    {
        Debug.Log(delay);
        if (delay < .7f)
        {
            delay += Time.deltaTime;
        }
        else
        {
            goToPlayer = true;
        }

        if (goToPlayer)
        {
            Vector3 dir = player.transform.position + new Vector3(0, 1f, 0) - this.transform.position;
            this.transform.position += dir.normalized * Time.deltaTime * speed;
        }
        else
        {
            // 하늘로 올라감
            this.transform.Translate(new Vector3(0, 1f, 0) * speed * Time.deltaTime);
        }
    }

    public void SetExp(int exp)
    {
        delay = 0;
        goToPlayer = false;

        // !!! Exp에 따라 컬러가 바뀌도록 수정 예정
        _exp = exp;
    }
}
