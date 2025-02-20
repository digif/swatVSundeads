﻿using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int hp = 1;
    public int damage = 1;

    private NavMeshAgent m_agent;
    private Animator m_Animator;
    private GameObject player;

    public bool hasFallen { get; private set; }

    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        hasFallen = false;
    }

    void Update()
    {
        AnimatorClipInfo[] m_CurrentClipInfo = m_Animator.GetCurrentAnimatorClipInfo(0);
        //Access the Animation clip name
        string m_ClipName = m_CurrentClipInfo[0].clip.name;
        Debug.Log(m_ClipName);
        if (m_ClipName == "Idle" || m_ClipName == "Walk" || m_ClipName == "idle" || m_ClipName == "walk" || m_ClipName == "walk_in_place")
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer < 1.5)
            {
                player.SendMessage("OnPlayerHit", damage);
                m_Animator.SetFloat("speed", 0);
                m_Animator.SetTrigger("Attack");
            }
            else
            {
                m_agent.SetDestination(player.transform.position);
                m_Animator.SetFloat("speed", 1);
            }
        }
    }

    public void OnHit()
    {
        hp -= 1;
        if (hp <= 0)
        {
            m_Animator.SetFloat("speed", 0);
            m_Animator.SetTrigger("Fall");
            hasFallen = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            m_Animator.SetFloat("speed", 0);
            m_Animator.SetTrigger("Hit");
        }

    }
}
