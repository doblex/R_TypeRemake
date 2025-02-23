using UnityEngine;
using UnityEngine.Splines;

public class EnemyFactory 
{
    public GameObject CreateEnemy(EnemyType enemyType, SplineContainer spline)
    {
        EnemyBuilder builder = new EnemyBuilder()
            .SetBasePrefab(enemyType.enemyPrefab)
            .SetSpline(spline)
            .SetSpeed(enemyType.speed);

        return builder.BuildOnLine();
    }

    public GameObject CreateEnemyPingPong(EnemyType enemyType, SplineContainer spline)
    {
        EnemyBuilder builder = new EnemyBuilder()
            .SetBasePrefab(enemyType.enemyPrefab)
            .SetSpline(spline)
            .SetSpeed(enemyType.speed);

        return builder.BuildPingPong();
    }
}
