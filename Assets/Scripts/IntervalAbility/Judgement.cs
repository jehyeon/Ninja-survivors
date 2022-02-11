using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : IntervalAbility
{
    private Player player;
    private float distance;

    private void Awake()
    {
        level = 1;
        timer = 0f;
        cooltime = 3f;

        distance = 10f;
    }

    public override void Excute()
    {
        // 주변 Enemy collider만 검출
        Collider[] nearColliders = Physics.OverlapSphere(player.transform.position, distance, 1 << 3);

        if (nearColliders.Length == 0)
        {
            // 주변 적이 없으면 실행 안됨
            return;
        }

        // temp
        // 나중에 해당 enemy 위에 enemy를 따라가는 프리팹 생성
        nearColliders[0].GetComponent<Enemy>().GetDamage(30);
    }

    public void GetPlayer(Player _player)
    {
        player = _player;
    }
}
