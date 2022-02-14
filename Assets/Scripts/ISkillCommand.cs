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
    private Transform playerTransform;
    
    private void Awake()
    {
        canExecute = true;
        cooltime = 5f;
    }

    public void Execute()
    {
        if (canExecute)
        {
            canExecute = false;
            InvokeRepeating("Move", 0f, 0.000016f);
            Invoke("CancelMove", .15f);
            StartCoroutine("StartCoolTime");
        }
    }

    IEnumerator StartCoolTime()
    {
        yield return new WaitForSeconds(cooltime);
        canExecute = true;
    }

    public void SetTransform(Transform transform)
    {
        playerTransform = transform;
    }

    public void Move()
    {
        playerTransform.Translate(Vector3.forward * 3);
    }

    public void CancelMove()
    {
        CancelInvoke("Move");
    }
}
