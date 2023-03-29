using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IAttackable
{
    #region EXPOSED_FIELDS
    [Header("Data")]
    [SerializeField] private EnemyData enemyData = null;

    [Space, Header("AI")]
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private LayerMask groundMask = default;
    [SerializeField] private LayerMask segmentMask = default;

    [Space, Header("UI")]
    [SerializeField] private HealthBar healthBar = null;
    #endregion

    #region PRIVATE_FIELDS
    private bool hasAttacked = false;

    private bool inAttackRange = false;

    private int maxHealth = 100;
    private int currentHealth = 100;
    private float attackRange = 10f;
    private float attackRate = 1f;
    private float speed = 10f;
    private int value = 1000;

    private Transform target = null;

    private bool initialized = false;
    #endregion

    #region ACTIONS
    private EconomyActions economyActions = null;
    private WavesControllerActions wavesControllerActions = null;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        if(!initialized)
        {
            return;
        }

        inAttackRange = Physics.CheckSphere(transform.position, attackRange - 1f, segmentMask);

        if (!inAttackRange)
        {
            Chase();
        }
        else
        {
            Attack();
        }
    }
    #endregion

    #region INIT
    public void Init(EconomyActions economyActions, WavesControllerActions wavesControllerActions)
    {
        this.economyActions = economyActions;
        this.wavesControllerActions = wavesControllerActions;

        maxHealth = enemyData.MaxHealth;
        currentHealth = enemyData.MaxHealth;
        attackRange = enemyData.AttackRange;
        attackRate = enemyData.AttackRate;
        speed = enemyData.Speed;
        value = enemyData.Value;

        agent.speed = speed;

        healthBar.Init(true);
        healthBar.UpdateTarget(maxHealth, maxHealth);

        initialized = true;
    }
    #endregion

    #region PUBLIC_METHODS
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        healthBar.UpdateTarget(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    #endregion

    #region PRIVATE_METHODS

    #region AI
    private void Chase()
    {
        if(target == null)
        {
            return;
        }

        IAttackable attackable = target.GetComponent<IAttackable>();

        if(attackable == null)
        {
            return;
        }

        agent.SetDestination(target.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        if (!hasAttacked)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, attackRange, segmentMask))
            {
                IAttackable attackable = hit.collider.GetComponent<IAttackable>();

                if (attackable != null)
                {
                    attackable.TakeDamage(10);
                    hasAttacked = true;
                    Invoke(nameof(ResetAttack), attackRate);
                }
            }
        }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }
    #endregion

    private void Die()
    {
        economyActions.onAddMoney?.Invoke(value);
        wavesControllerActions.onReduceEnemyAlive?.Invoke();

        Destroy(gameObject);
    }
    #endregion
}
