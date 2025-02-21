using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform parentTransform;
    [SerializeField] Vector3 relativePos;

    List<GateController> gates;

    List<GameObject> currentSpawnersPrefabs = new List<GameObject>();

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

        GameObject.FindGameObjectWithTag("Player").GetComponent<HealtController>().onDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        for (int i = 0; i < gates.Count; i++)
        {
            gates[i].onTouchGate -= OnTouchGate;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<HealtController>().onDeath -= OnPlayerDeath;

    }

    void OnPlayerDeath(GameObject gameObject) 
    { 
        //Restartenu
    }

    void OnTouchGate(List<GameObject> prefabSpawners)
    {
        AddSpawners(prefabSpawners);
        ChangeLane();
    }

    private void AddSpawners(List<GameObject> prefabSpawners)
    {
        for (int i = 0; i < prefabSpawners.Count; i++)
        {
            GameObject clone = GameObject.Instantiate(prefabSpawners[i], parentTransform.position + relativePos, Quaternion.identity, parentTransform);
            currentSpawnersPrefabs.Add(clone);

            EnemySpawner enemySpawner = clone.GetComponent<EnemySpawner>();
            enemySpawner.onSpawnEnded += OnSpawnEnded;

        }
    }

    void OnSpawnEnded(GameObject prefabSpawner)
    { 
        EnemySpawner enemySpawner = prefabSpawner.GetComponent<EnemySpawner>();
        enemySpawner.onSpawnEnded -= OnSpawnEnded;

        currentSpawnersPrefabs.Remove(prefabSpawner);
        Destroy(prefabSpawner);
    }

    private void ChangeLane()
    {
        Debug.Log("Lane Changed");
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
