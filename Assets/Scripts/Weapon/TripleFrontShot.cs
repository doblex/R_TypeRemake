using UnityEngine;

[CreateAssetMenu(menuName = "TripleFrontShot", fileName = "WeaponStrategy/TripleFrontShot")]
public class TripleFrontShot : WeaponStrategy
{

    public override void Fire(Transform firePoint, LayerMask layer)
    {

        for (float i = -1; i < 2; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position + new Vector3 (0, i * 0.3f, 0), firePoint.rotation);
            projectile.transform.SetParent(firePoint);

            projectile.layer = layer;

            Projectile projectileComponent = projectile.GetComponent<Projectile>();

            projectileComponent.SetSpeed(projectileSpeed);
            projectileComponent.SetDamage(damage);

            Destroy(projectile, projectileLifeTime);
        }
    }
}
