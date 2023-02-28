using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    #region EXPOSED_FIELDS
    [SerializeField] private string id = string.Empty;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackRate = 0.5f;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private int value = 1000;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public int MaxHealth { get => maxHealth; }
    public float AttackRange { get => attackRange; }
    public float AttackRate { get => attackRate; }
    public float Speed { get => speed; }
    public int Value { get => value; }
    #endregion
}
