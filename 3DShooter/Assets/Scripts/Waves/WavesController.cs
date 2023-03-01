using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesControllerActions
{
    public Action onCheckEnemiesAlive = null;
    public Action onReduceEnemyAlive = null;
}

public class WavesController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private WaveData[] wavesData = null;
    [SerializeField] private Transform[] spawnPoints = null;
    #endregion

    #region PRIVATE_FIELDS
    private int index = 0;

    private int enemiesAlive = 0;

    private bool spawnEnemies = false;
    #endregion

    #region ACTIONS
    private WavesControllerActions wavesControllerActions = new();
    private NexusControllerActions nexusControllerActions = null;
    private EconomyActions economyActions = null;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            spawnEnemies = true;
            CheckEnemiesAlive();
        }
    }
    #endregion

    #region INIT
    public void Init(NexusControllerActions nexusControllerActions, EconomyActions economyActions)
    {
        this.nexusControllerActions = nexusControllerActions;
        this.economyActions = economyActions;

        wavesControllerActions.onCheckEnemiesAlive += CheckEnemiesAlive;
        wavesControllerActions.onReduceEnemyAlive += ReduceEnemyAlive;
    }
    #endregion

    #region PUBLIC_METHODS
    public WavesControllerActions GetActions()
    {
        return wavesControllerActions;
    }
    #endregion

    #region PRIVATE_METHODS
    private void SpawnEnemy(GameObject enemyPrefab)
    {
        EnemyAI enemyAI = Instantiate(enemyPrefab, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)]).GetComponent<EnemyAI>();

        enemyAI.Init(nexusControllerActions, economyActions, wavesControllerActions);
    }

    private IEnumerator ISpawnWave(WaveData waveData)
    {
        enemiesAlive = waveData.Amount;

        for(int i = 0; i < waveData.Amount; i++)
        {
            SpawnEnemy(waveData.EnemyPrefab.gameObject);

            yield return new WaitForSeconds(1f / waveData.SpawnRate);
        }

        SetNextIndex();

        yield break;
    }

    private void CheckEnemiesAlive()
    {
        if(enemiesAlive <= 0)
        {
            StartCoroutine(ISpawnWave(wavesData[index]));
        }
    }

    private void SetNextIndex()
    {
        index++;

        if(index >= wavesData.Length)
        {
            index = 0;
            spawnEnemies = false;
        }
    }

    private void ReduceEnemyAlive()
    {
        enemiesAlive--;

        if(spawnEnemies)
        {
            wavesControllerActions.onCheckEnemiesAlive?.Invoke();
        }        
    }
    #endregion
}
