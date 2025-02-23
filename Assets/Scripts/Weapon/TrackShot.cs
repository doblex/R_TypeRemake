using UnityEngine;

[CreateAssetMenu(menuName = "TrackShot", fileName = "WeaponStrategy/TrackShot")]
public class TrackShot : WeaponStrategy
{
    GameObject _Player;

    public override void Initialize()
    {
        _Player = GameObject.FindWithTag("Player");
    }

    public override void Fire(Transform firePoint, LayerMask layer)
    {
        Vector2 targetPos = _Player.transform.position - firePoint.position;

        //Quaternion targetRotation = Quaternion.LookRotation(targetPos).normalized;

        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, targetRotation);
        projectile.transform.SetParent(firePoint);

        projectile.layer = layer;

        Projectile projectileComponent = projectile.GetComponent<Projectile>();

        projectileComponent.SetSpeed(projectileSpeed);
        projectileComponent.SetDamage(damage);

        Destroy(projectile, projectileLifeTime);
    }
}
