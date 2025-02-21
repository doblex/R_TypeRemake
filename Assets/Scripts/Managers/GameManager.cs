using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject Cam;

    public float animDuration = 3f;

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

        player.GetComponent<HealtController>().onDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        for (int i = 0; i < gates.Count; i++)
        {
            gates[i].onTouchGate -= OnTouchGate;
        }

        player.GetComponent<HealtController>().onDeath -= OnPlayerDeath;

    }

    void OnPlayerDeath(GameObject gameObject) 
    { 
        //Restartenu
    }

    void OnTouchGate(List<GameObject> prefabSpawners, Transform nextStart)
    {
        AddSpawners(prefabSpawners);
        if(nextStart != null)
            ChangeLane(nextStart);
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

    private void ChangeLane(Transform nextStart)
    {
        LockPlayerAndCamera();
        StartCoroutine(ChangeLane(animDuration, nextStart));
    }

    IEnumerator ChangeLane(float duration, Transform nextStart)
    {
        while (duration > 0f)
        {
            player.transform.position = Vector2.Lerp(player.transform.position, nextStart.position, duration / Time.deltaTime);
            duration -= Time.deltaTime;
        }

        player.transform.position = nextStart.position;
        Cam.transform.position = new Vector3(nextStart.position.x, nextStart.position.y, Cam.transform.position.z);
        LockPlayerAndCamera(false);
        yield return null;
    }

    private void LockPlayerAndCamera(bool locked = true)
    {
        Cam.GetComponent<CameraController>().isLocked = locked;
        player.GetComponent<PlayerController>().isLocked = locked;
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
