using UnityEngine;

[CreateAssetMenu(menuName = "TripleShot", fileName = "WeaponStrategy/TripleShot")]
public class TripleShot : WeaponStrategy
{
    [SerializeField] float angleBetweenShots = 15;

    public override void Fire(Transform firePoint, LayerMask layer)
    {
        for (int i = -1; i < 2; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0,0,angleBetweenShots * i));
            projectile.transform.SetParent(firePoint);

            projectile.layer = layer;

            Projectile projectileComponent = projectile.GetComponent<Projectile>();

            projectileComponent.SetSpeed(projectileSpeed);
            projectileComponent.SetDamage(damage);

            Destroy(projectile, projectileLifeTime);
        }
    }
}
