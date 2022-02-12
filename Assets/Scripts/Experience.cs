using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public int _exp;
    private static float speed = 15f;
    private float delay;

    private bool goToPlayer;
    private GameObject player;
    private ObjectPool expOP;
    
    [SerializeField]
    private ParticleSystem mainPs;    // main particle
    [SerializeField]
    private ParticleSystem subPs;    // sub particle

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
            other.gameObject.GetComponent<Player>().GainExp(_exp);
            expOP.Return(this.gameObject);
        }

        return;
    }

    private void GoToPlayer()
    {
        if (delay < .5f)
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

            this.transform.rotation = Quaternion.LookRotation(dir);
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

        _exp = exp;
        UpdateColor();
    }

    private void UpdateColor()
    {
        var main = mainPs.main;
        var sub = subPs.main;

        if (_exp <= 10)
        {
            // Green
            main.startColor = new Color(125f / 255f, 1f, 125f / 255f, 1f);
            // sub.startColor = new Color(125f / 255f, 1f, 125f / 255f, 1f);
        }
        else if (_exp <= 50)
        {
            // blue
            main.startColor = new Color(50f / 255f, 50f / 255f, 180f / 255f, 1f);
            // sub.startColor = new Color(0f, 0f, 180f / 255f, 1f);

            // pink
            // main.startColor = new Color(50f, 0f, 150f, 255f);
            // sub.startColor = new Color(50f, 0f, 150f, 255f);
            // sub.startColor = new Color(90f, 30f, 190f, 255f);
        }
        else if (_exp <= 100)
        {
            // red
            main.startColor = new Color(150f / 255f, 50f / 255f, 50f / 255f, 1f);
            // sub.startColor = new Color(180f/ 255f, 50f/ 255f, 50f/ 255f, 1f);
        }
    }
}
