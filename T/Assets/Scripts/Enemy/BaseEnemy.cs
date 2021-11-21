using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    [SerializeField]
    protected int health;

    [SerializeField]
    protected int maxHealth;

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float sightRange;

    [Header("Attack")]

    [SerializeField]
    protected float timeBitweenAttacks;

    [SerializeField]
    protected float attackRange;

    protected virtual void TakeDamage()
    {

    }

    protected virtual void Patroling()
    {

    }

    protected virtual void Chase()
    {

    }

    protected virtual void Attack()
    {

    }
}
