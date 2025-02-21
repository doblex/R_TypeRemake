using UnityEngine;

public class EnemyWeapon : Weapon
{
    float fireTimer;

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if(fireTimer >= weaponStrategy.FireRate)
        {
            weaponStrategy.Fire(firePoint, layer);
            fireTimer = 0;
        }
    }
}