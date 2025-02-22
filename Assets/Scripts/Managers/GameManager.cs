using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public delegate void OnEndGame();

    public static OnEndGame onEndGame;

    public static GameManager instance;

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject Cam;

    public float animDuration = 3f;

    [SerializeField] Transform parentTransform;
    [SerializeField] Vector3 relativePos;

    List<GateController> gates;

    List<GameObject> currentSpawnersPrefabs = new List<GameObject>();

    bool isAnimating;
    float duration;
    Transform nextStart;

    [Header("Ui Docs")]
    [SerializeField] UIDocument pauseDoc;
    bool isPaused;

    [SerializeField] UIDocument HealtBar;

    [SerializeField] UIDocument EndGame;


    private void OnValidate()
    {
        gates = new List<GateController>(GameObject.FindObjectsByType<GateController>(FindObjectsSortMode.InstanceID));
    }

    private void Awake()
    {
        SetInstance();
        isAnimating = false;
        pauseDoc.rootVisualElement.style.display = DisplayStyle.None;
        EndGame.rootVisualElement.style.display = DisplayStyle.None;
        isPaused = false;
    }

    private void OnEnable()
    {
        for (int i = 0; i < gates.Count; i++)
        {
            gates[i].onTouchGate += OnTouchGate;
        }

        player.GetComponent<HealthController>().onDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        for (int i = 0; i < gates.Count; i++)
        {
            gates[i].onTouchGate -= OnTouchGate;
        }
        player.GetComponent<HealthController>().onDeath -= OnPlayerDeath;
    }

    private void Update()
    {
        CheckPause();
        ChangeLaneAnim();
    }

    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            isPaused = !isPaused;
            pauseDoc.rootVisualElement.style.display = isPaused ? DisplayStyle.Flex : DisplayStyle.None;
            HealtBar.rootVisualElement.style.display = isPaused ? DisplayStyle.None : DisplayStyle.Flex;
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }

    void OnPlayerDeath(GameObject gameObject) 
    {
        HealtBar.rootVisualElement.style.display = DisplayStyle.None;
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
            if (enemySpawner.isBossSpawner)
            {
                enemySpawner.onSpawnEnded += OnSpawnerEndGame;
            }
            else
            {
                enemySpawner.onSpawnEnded += OnSpawnEnded;
            }
        }
    }

    private void OnSpawnerEndGame(GameObject prefabSpawner)
    {
        EnemySpawner enemySpawner = prefabSpawner.GetComponent<EnemySpawner>();
        enemySpawner.onSpawnEnded -= OnSpawnEnded;

        currentSpawnersPrefabs.Remove(prefabSpawner);
        Destroy(prefabSpawner);
        onEndGame?.Invoke();
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
        duration = animDuration;
        this.nextStart = nextStart;

    }

    void ChangeLaneAnim()
    {
        if (isAnimating)
        { 
            player.transform.position = Vector3.Lerp(player.transform.position, nextStart.position, duration * Time.deltaTime);
            duration -= Time.deltaTime;

            if (duration <= 0)
            { 
                player.transform.position = nextStart.position;
                Cam.transform.position = new Vector3(nextStart.position.x, nextStart.position.y, Cam.transform.position.z);
                LockPlayerAndCamera(false);
            }
        }
    }

    private void LockPlayerAndCamera(bool locked = true)
    {
        Cam.GetComponent<CameraController>().isLocked = locked;
        player.GetComponent<PlayerController>().isLocked = locked;
        isAnimating = locked;
    }


    public void SetInstance() 
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else 
        { 
            Destroy(gameObject);
        }
    }
}
