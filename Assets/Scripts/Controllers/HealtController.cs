using UnityEngine;

public class HealtController : MonoBehaviour
{
    public delegate void OnDeath(GameObject gameObject);

    public OnDeath onDeath;

    [SerializeField] int MaxHitPoints;

    int currentHp;

    private void Start()
    {
        currentHp = MaxHitPoints;
    }

    public void DoDamage(int damage) 
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            onDeath?.Invoke(gameObject);
        }
    }
}
