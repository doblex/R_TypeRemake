using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Scriptable Objects/EnemyType")]
public class EnemyType : ScriptableObject
{
    public GameObject enemyPrefab;
    public GameObject weaponPrefab;
    public float speed;
}
