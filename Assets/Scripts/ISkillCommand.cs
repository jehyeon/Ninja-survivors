using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillCommand
{
    public void Execute();
}

public class Dash : MonoBehaviour, ISkillCommand
{
    private bool canExecute;
    private float cooltime;
    private GameObject go_player;
    private GameObject cameraRotate;
    private Katana katana;
    private Animator animator;
    
    private void Awake()
    {
        canExecute = true;
        cooltime = 0f;
    }

    public void Execute()
    {
        if (canExecute)
        {
            canExecute = false;
            // 대쉬 애니메이션
            animator.SetTrigger("isDash");
            // 이동
            katana.ActivateCollider();
            InvokeRepeating("Move", 0f, 0.0016f);
            Invoke("CancelMove", .15f);

            // 공격 활성화
            StartCoroutine("StartCoolTime");
        }
    }

    IEnumerator StartCoolTime()
    {
        yield return new WaitForSeconds(cooltime);
        canExecute = true;
    }

    public void SetPlayer(GameObject player, GameObject _cameraRotate)
    {
        go_player = player;
        cameraRotate = _cameraRotate;
        katana = (Katana)player.GetComponent<WeaponSystem>().Weapon;
        animator = player.GetComponent<Animator>();
    }

    public void Move()
    {
        go_player.transform.Translate(Vector3.forward * 3 + new Vector3(0, cameraRotate.transform.forward.y, 0));
    }

    public void CancelMove()
    {
        CancelInvoke("Move");
        Invoke("DelayDeActivateCollider", 0.3f);
    }

    private void DelayDeActivateCollider()
    {
        katana.DeActivateCollider();
    }
}
