using UnityEngine;

public class HealthController : MonoBehaviour
{
    public delegate void OnDeath(GameObject gameObject);
    public delegate void OnDamage(float MaxHp, float currentHp);

    public OnDeath onDeath;
    public OnDamage onDamage;

    [SerializeField] int MaxHitPoints;

    int currentHp;

    private void Start()
    {
        currentHp = MaxHitPoints;
        onDamage?.Invoke(MaxHitPoints, currentHp);
    }

    public void DoDamage(int damage) 
    {
        currentHp -= damage;
        onDamage?.Invoke(MaxHitPoints, currentHp);

        if (currentHp <= 0)
        {
            onDeath?.Invoke(gameObject);
        }
    }
}
