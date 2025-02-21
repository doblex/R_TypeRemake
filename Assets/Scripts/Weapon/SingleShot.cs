using UnityEngine;


[CreateAssetMenu(menuName = "SingleShot", fileName = "WeaponStrategy/SingleShot")]
public class SingleShot : WeaponStrategy 
{
    public override void Fire(Transform firePoint, LayerMask layer)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.transform.SetParent(firePoint);

        projectile.layer = layer;

        Projectile projectileComponent = projectile.GetComponent<Projectile>();

        projectileComponent.SetSpeed(projectileSpeed);
        projectileComponent.SetDamage(damage);

        Destroy(projectile, projectileLifeTime);
    }
}
