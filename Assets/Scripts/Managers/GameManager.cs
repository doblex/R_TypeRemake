using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform cam;

    List<GateController> gates;

    List<GameObject> currentSpawnersPrefabs;
    List<GameObject> nextSpawnersPrefabs;

    private bool isFirstWave;

    private void OnValidate()
    {
        gates = new List<GateController>(GameObject.FindObjectsByType<GateController>(FindObjectsSortMode.InstanceID));
    }

    private void Awake()
    {
        SetInstance();
    }

    private void OnEnable()
    {
        for (int i = 0; i < gates.Count; i++)
        {
            gates[i].onTouchGate += OnTouchGate;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < gates.Count; i++)
        {
            gates[i].onTouchGate -= OnTouchGate;
        }
    }

    private void Start()
    {
        isFirstWave = true;
    }

    void OnTouchGate(List<GameObject> prefabSpawners)
    {
        nextSpawnersPrefabs = prefabSpawners;
        if (isFirstWave)
        {
            isFirstWave = false;
            InstatiateNextSpawners();
        }
        else 
        {
            ChangeLane();
            DisableCurrentSpawners();
        }
    }

    void OnSpawnEnded(GameObject prefabSpawner)
    { 
        EnemySpawner enemySpawner = prefabSpawner.GetComponent<EnemySpawner>();
        enemySpawner.onSpawnEnded -= OnSpawnEnded;

        currentSpawnersPrefabs.Remove(prefabSpawner);
        Destroy(prefabSpawner);

        if (currentSpawnersPrefabs.Count > 0)
            InstatiateNextSpawners();
    }

    private void InstatiateNextSpawners()
    {
        //Creo una nuova lista partendo dagli nuovi spawners
        currentSpawnersPrefabs = new List<GameObject>(nextSpawnersPrefabs);
        nextSpawnersPrefabs.Clear();

        //subscribe per sapere quando terminano di spawnare
        for (int i = 0; i < currentSpawnersPrefabs.Count; i++)
        {
            GameObject.Instantiate(currentSpawnersPrefabs[i], Vector3.zero, Quaternion.identity, cam);
            EnemySpawner enemySpawner = currentSpawnersPrefabs[i].GetComponent<EnemySpawner>();
            enemySpawner.onSpawnEnded += OnSpawnEnded;
        }
    }

    private void DisableCurrentSpawners()
    {
        for (int i = 0; i < currentSpawnersPrefabs.Count; i++)
        {
            EnemySpawner enemySpawner = currentSpawnersPrefabs[i].GetComponent<EnemySpawner>();
            enemySpawner.isDisabled = true;
        }
    }

    private void ChangeLane()
    {
    }

    public void SetInstance() 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        { 
            Destroy(gameObject);
        }
    }
}
