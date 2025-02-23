using UnityEngine;

[CreateAssetMenu(menuName = "BossShot", fileName = "WeaponStrategy/BossShot")]
public class BossShot : WeaponStrategy
{
    [SerializeField] float angleBetweenShots = 10;

    public override void Fire(Transform firePoint, LayerMask layer)
    {
        for (int i = -4; i < 5; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, angleBetweenShots * i));
            projectile.transform.SetParent(firePoint);

            projectile.layer = layer;

            Projectile projectileComponent = projectile.GetComponent<Projectile>();

            projectileComponent.SetSpeed(projectileSpeed);
            projectileComponent.SetDamage(damage);

            Destroy(projectile, projectileLifeTime);
        }
    }
}
