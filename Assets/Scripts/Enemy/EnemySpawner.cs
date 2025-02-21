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

    [SerializeField] List<EnemyType> enemyTypes;
    [SerializeField] int maxEnemies = 10;
    [SerializeField] int EnemiesLimit = 10;
    [SerializeField] float spawnInterval = 1.0f;

    List<SplineContainer> splines;
    EnemyFactory enemyFactory;

    float spawnTimer;
    int enemiesSpawned;
    int aliveEnemies = 0;

    private void Start()    
    {
        splines = new List<SplineContainer>(GetComponentsInChildren<SplineContainer>());
        enemyFactory = new EnemyFactory();
    }

    private void Update()
    {
        TrySpawn();
        checkSpawnEnded();
    }

    private void TrySpawn()
    {
        spawnTimer += Time.deltaTime;

        if (enemiesSpawned < maxEnemies && aliveEnemies < EnemiesLimit && spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    private void checkSpawnEnded() 
    {
        if (enemiesSpawned >= maxEnemies && aliveEnemies <= 0)
        {
            onSpawnEnded?.Invoke(gameObject);
        }
    }

    private void SpawnEnemy()
    {
        EnemyType enemyType = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count)];
        SplineContainer spline = splines[UnityEngine.Random.Range(0, splines.Count)];

        GameObject clone = enemyFactory.CreateEnemy(enemyType, spline);
        clone.GetComponent<HealtController>().onDeath += OnDeath;

        enemiesSpawned++;
        aliveEnemies++;
    }

    void OnDeath(GameObject gameObject)
    { 
        gameObject.GetComponent<HealtController>().onDeath -= OnDeath;
        Destroy(gameObject);
        aliveEnemies--;
    }
}
