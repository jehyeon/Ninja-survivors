using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Stat _stat;
    public Stat Stat { get { return _stat; } }

    protected Animator animator;

    protected virtual void Awake()
    {
        _stat = gameObject.AddComponent<Stat>();
        animator = GetComponent<Animator>();
    }
}
