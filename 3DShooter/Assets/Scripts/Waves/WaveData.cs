using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave_", menuName = "ScriptableObjects/Wave", order = 1)]
public class WaveData : ScriptableObject
{
    #region EXPOSED_FIELDS
    [SerializeField] private string id = string.Empty;
    [SerializeField] private EnemyAI enemyPrefab = null;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private int amount = 5;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public EnemyAI EnemyPrefab { get => enemyPrefab; }
    public float SpawnRate { get => spawnRate; }
    public int Amount { get => amount; }
    #endregion
}
