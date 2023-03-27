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
    private EconomyActions economyActions = null;
    private SegmentsControllerActions segmentsControllerActions = null;
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
    public void Init(EconomyActions economyActions, SegmentsControllerActions segmentsControllerActions)
    {
        this.economyActions = economyActions;
        this.segmentsControllerActions = segmentsControllerActions;

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
    private void SpawnEnemy(GameObject enemyPrefab, Transform[] segments)
    {
        EnemyAI enemyAI = Instantiate(enemyPrefab, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)]).GetComponent<EnemyAI>();

        enemyAI.Init(economyActions, wavesControllerActions);
        enemyAI.SetTarget(GetNearestTarget(enemyAI.transform, segments));
    }

    private IEnumerator ISpawnWave(WaveData waveData)
    {
        enemiesAlive = waveData.Amount;

        Transform[] segments = segmentsControllerActions.onGetSegmentsTransforms?.Invoke();

        for(int i = 0; i < waveData.Amount; i++)
        {
            SpawnEnemy(waveData.EnemyPrefab.gameObject, segments);

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

    private Transform GetNearestTarget(Transform enemy, Transform[] transforms)
    {
        int index = 0;

        float nearestDistance = -1f;

        for(int i = 0; i < transforms.Length; i++)
        {
            float distance = Vector3.Distance(enemy.position, transforms[i].position);

            if(nearestDistance == -1f)
            {
                nearestDistance = distance;
                index = 0;
                continue;
            }

            if(distance < nearestDistance)
            {
                nearestDistance = distance;
                index = i;
            }
        }

        return transforms[index];
    }
    #endregion
}
