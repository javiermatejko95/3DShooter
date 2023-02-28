using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesControllerActions
{
    public Action onCheckEnemiesAlive = null;
}

public class WavesController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private WaveData[] wavesData = null;
    [SerializeField] private float waveRate = 5f;
    [SerializeField] private Transform spawnPoint = null;
    #endregion

    #region PRIVATE_FIELDS
    private int index = 0;
    private float waveCountdown = 0f;

    private bool initialized = false;
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

        Restart();
    }
    #endregion

    #region PUBLIC_METHODS
    public WavesControllerActions GetActions()
    {
        return wavesControllerActions;
    }
    #endregion

    #region PRIVATE_METHODS
    private void Restart()
    {
        waveCountdown = waveRate;
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        EnemyAI enemyAI = Instantiate(enemyPrefab).GetComponent<EnemyAI>();

        enemyAI.Init(nexusControllerActions, economyActions, wavesControllerActions);
    }

    private IEnumerator ISpawnWave(WaveData waveData)
    {
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
        if(GameObject.FindGameObjectWithTag("Enemy") == null)
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
        }
    }
    #endregion
}
