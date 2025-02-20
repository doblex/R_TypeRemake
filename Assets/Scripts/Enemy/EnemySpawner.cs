using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    //delegate
    public delegate void OnSpawnEnded(GameObject spawnerPrefab);

    //Event
    public OnSpawnEnded onSpawnEnded;

    public bool isDisabled { get; set; }

    [SerializeField] List<EnemyType> enemyTypes;
    [SerializeField] int maxEnemies = 10;
    [SerializeField] float spawnInterval = 1.0f;

    List<SplineContainer> splines;
    EnemyFactory enemyFactory;

    float spawnTimer;
    int enemiesSpawned;

    private void Start()    
    {
        splines = new List<SplineContainer>(GetComponentsInChildren<SplineContainer>());
        enemyFactory = new EnemyFactory();
        isDisabled = false;
    }

    private void Update()
    {
        TrySpawn();
        checkSpawnEnded();
    }

    private void TrySpawn()
    {
        spawnTimer += Time.deltaTime;

        if (!isDisabled && enemiesSpawned < maxEnemies && spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    private void checkSpawnEnded() 
    {
        if (isDisabled && enemiesSpawned <= 0)
        {
            onSpawnEnded?.Invoke(gameObject);
        }
    }

    private void SpawnEnemy()
    {
        EnemyType enemyType = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count)];
        SplineContainer spline = splines[UnityEngine.Random.Range(0, splines.Count)];

        enemyFactory.CreateEnemy(enemyType, spline);

        //TODO: subscribe to OnDeath Event to decrease the enemiesSpawned
        enemiesSpawned++;
    }
}
