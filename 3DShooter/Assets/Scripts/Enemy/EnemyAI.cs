using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [Header("Data")]
    [SerializeField] private EnemyData enemyData = null;

    [Space, Header("AI")]
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private LayerMask groundMask = default;
    [SerializeField] private LayerMask nexusMask = default;

    [Space, Header("UI")]
    [SerializeField] private HealthBar healthBar = null;
    #endregion

    #region PRIVATE_FIELDS
    private Nexus nexus = null;

    private bool hasAttacked = false;

    private bool inAttackRange = false;

    private int maxHealth = 100;
    private int currentHealth = 100;
    private float attackRange = 10f;
    private float attackRate = 1f;
    private float speed = 10f;
    private int value = 1000;

    private bool initialized = false;
    #endregion

    #region ACTIONS
    private NexusControllerActions nexusControllerActions = null;
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

        inAttackRange = Physics.CheckSphere(transform.position, attackRange, nexusMask);

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
    public void Init(NexusControllerActions nexusControllerActions, EconomyActions economyActions, WavesControllerActions wavesControllerActions)
    {
        this.nexusControllerActions = nexusControllerActions;
        this.economyActions = economyActions;
        this.wavesControllerActions = wavesControllerActions;

        this.nexus = nexusControllerActions.onGetNexus?.Invoke();

        maxHealth = enemyData.MaxHealth;
        currentHealth = enemyData.MaxHealth;
        attackRange = enemyData.AttackRange;
        attackRate = enemyData.AttackRate;
        speed = enemyData.Speed;
        value = enemyData.Value;

        agent.speed = speed;

        healthBar.Init();
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
    #endregion

    #region PRIVATE_METHODS

    #region AI
    private void Chase()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Segment");

        agent.SetDestination(go.transform.position);
        //agent.SetDestination(nexus.transform.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        if (!hasAttacked)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;            

            if (Physics.Raycast(ray, out hit, attackRange, nexusMask))
            {
                IAttackable attackable = hit.collider.GetComponent<IAttackable>();

                if (attackable != null)
                {
                    attackable.TakeDamage(10);
                }
            }

            

            //nexusControllerActions.onTakeDamage?.Invoke(10);

            hasAttacked = true;
            Invoke(nameof(ResetAttack), attackRate);
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
