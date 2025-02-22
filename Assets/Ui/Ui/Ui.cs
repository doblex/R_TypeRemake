using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Ui : MonoBehaviour
{
    [SerializeField] Color maxHpColor;
    [SerializeField] Color minHpColor;
    
    GameObject player;

    UIDocument doc;
    VisualElement root;

    VisualElement healthBar;
    Label healthLabel;

    private void Awake()
    {
        doc = GetComponent<UIDocument>();
        root = doc.rootVisualElement;

        healthBar = root.Q<VisualElement>("fill");
        healthLabel = root.Q<Label>("healthText");
    }

    private void OnEnable()
    {
        player.GetComponent<HealthController>().onDamage += OnPlayerDamage;
    }

    private void OnDisable()
    {
        player.GetComponent<HealthController>().onDamage -= OnPlayerDamage;
    }

    void OnPlayerDamage(float MaxHp, float currentHp)
    { 
        healthLabel.text = currentHp.ToString() + "/" + MaxHp.ToString();

        healthBar.style.backgroundColor = Color.Lerp(minHpColor, maxHpColor, currentHp / MaxHp);
        healthBar.style.width = Length.Percent( Mathf.Lerp(0,100, currentHp / MaxHp));
    }
}
