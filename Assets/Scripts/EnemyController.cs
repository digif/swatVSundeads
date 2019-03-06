using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator m_Animator;

    private bool startMouseRotate;
    private Vector3 prevMousePosition;
    private AnimatorClipInfo[] m_CurrentClipInfo;
    private string m_ClipName;
    private int distanceToPlayer;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        m_CurrentClipInfo = this.m_Animator.GetCurrentAnimatorClipInfo(0);
        //Access the Animation clip name
        m_ClipName = m_CurrentClipInfo[0].clip.name;
        if (m_ClipName == "Idle - Walk" && distanceToPlayer < 1)
        {
            m_Animator.SetTrigger("Attack");
        }
        m_Animator.SetTrigger("Hit");

        m_Animator.SetTrigger("Fall");

        //m_Animator.SetFloat("speedv", speed);
    }
}
