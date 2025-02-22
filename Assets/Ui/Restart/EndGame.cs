using Mono.Cecil;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndGame : MonoBehaviour
{
    [SerializeField] UIDocument doc;
    VisualElement root;

    [SerializeField] GameObject player;

    Button retry;
    Button quit;

    private void Awake()
    {
        root = doc.rootVisualElement;
        retry = root.Q<Button>("Retry");
        retry.clicked += Retry_clicked;

        quit = root.Q<Button>("Quit");
        quit.clicked += Quit_clicked; ;
    }

    private void Quit_clicked()
    {
        Time.timeScale = 1.0f;
        player.GetComponent<HealthController>().onDeath -= OnPlayerDeath;
        GameManager.onEndGame -= OnEndGame;
        SceneManager.LoadScene("Title");
    }

    private void Retry_clicked()
    {
        Time.timeScale = 1.0f;
        player.GetComponent<HealthController>().onDeath -= OnPlayerDeath;
        GameManager.onEndGame -= OnEndGame;
        SceneManager.LoadScene("Level1");
    }

    private void OnEnable()
    {
        player.GetComponent<HealthController>().onDeath += OnPlayerDeath;
        GameManager.onEndGame += OnEndGame;
    }

    private void OnDisable()
    {

    }

    void OnPlayerDeath(GameObject gameObject) 
    {
        Time.timeScale = 0f;

        root.style.display = DisplayStyle.Flex;
        root.Q<Label>().text = "You have failed\r\n the mission";
    }
    private void OnEndGame()
    {
        Time.timeScale = 0;

        root.style.display = DisplayStyle.Flex;
        root.Q<Label>().text = "You have succeeded\r\n the mission";
    }
}
