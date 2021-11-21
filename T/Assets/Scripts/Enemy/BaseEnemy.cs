using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected Transform player;

    [SerializeField]
    protected int health;

    [SerializeField]
    protected int maxHealth;

    [SerializeField]
    protected float patrolSpeed;

    [SerializeField]
    protected float chaseSpeed;

    [SerializeField]
    protected float sightRange;

    [Header("Patroling")]
    
    [SerializeField]
    
    protected Vector3 walkPoint;
   
    bool walkPointSet;
    
    [SerializeField]
    protected float walkPointRange;

    [Header("Attack")]

    [SerializeField]
    protected float timeBitweenAttacks;
    public bool alreadyAttacked;
    [SerializeField]
    protected float attackRange;

    [SerializeField]
    protected LayerMask isGround, isPlayer;

    protected bool playerIsInSightRange;
    protected bool playerIsInAttackingRange;

    protected void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        player = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected void Update()
    {
        CheckRanges();
        HadleBehavior();
    }

    private void HadleBehavior()
    {
        if (!playerIsInSightRange && !playerIsInAttackingRange) Patroling();
        if (playerIsInSightRange && !playerIsInAttackingRange) BaseChase();
        if (playerIsInSightRange && playerIsInAttackingRange) BaseAttack();
    }

    private void CheckRanges()
    {
        playerIsInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerIsInAttackingRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);
    }

    //Override this in the child class of the enemy if u need
    public virtual void TakeDamage(int damage)
    {
        Debug.Log("Enemy Damaged");
        health -= damage;
        if (health <= 0) Dying();
    }

    protected virtual void Patroling()
    {
        navMeshAgent.speed = patrolSpeed;
        if (!walkPointSet) CreateWalkPoint();

        if (walkPointSet)
            navMeshAgent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 2f)
        {
            walkPointSet = false;
        }
    }

    private void CreateWalkPoint()
    {
        float randX = Random.Range(-walkPointRange, walkPointRange);
        float randZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randX,
            transform.position.y,
            transform.position.z + randZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            walkPointSet = true;
        }
    }

    protected virtual void BaseChase()
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(player.position);
    }

    protected virtual void BaseAttack()
    {
        navMeshAgent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Attack();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBitweenAttacks);
        }
    }

    protected virtual void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //Override in the child class
    protected virtual void Attack()
    {
        Debug.Log("Set the attacking");
    }
    public virtual void Dying()
    {
        Destroy(gameObject);
    }
}
