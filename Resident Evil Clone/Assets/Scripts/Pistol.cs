using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Pistol : Weapon
{
    private void Start()
    {
        canFire = true; 
        ammoCapacity = 10;
        damage = 1;
        reloadTime = 2;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Fire()
    {
        if (canFire)
        {
            if (currentAmmo > 0)
            {
                Debug.Log("Pistol Fired");
                currentAmmo--;
                RaycastHit hit;
                if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100))
                {
                    Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
                    if (hit.transform.CompareTag("Zombie"))
                    {
                        hit.transform.GetComponent<Zombie>().TakeDamage(damage);
                    }
                }
            }
            else
            {
                if (currentSpareAmmo > 0)
                {
                    Reload();
                }
                else
                {
                    Debug.Log("Out of Ammo.");
                }
            }
        }
        else
        {
            Debug.Log("Still Reloading...");
        }
    }

    protected override void Reload()
    {
        int ammoReload = ammoCapacity - currentAmmo;
        if (ammoReload == 0)
        {
            Debug.Log("Pistol already full.");
        }
        else
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        canFire = false;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        canFire = true;

        int ammoReload = ammoCapacity - currentAmmo;
        
        if (ammoReload > currentSpareAmmo)
        {
            Debug.Log("Reloaded " + currentSpareAmmo);
            currentAmmo += currentSpareAmmo;
            currentSpareAmmo = 0;
        }
        else
        {
            Debug.Log("Reloaded " + ammoReload);
            currentAmmo = ammoCapacity;
            currentSpareAmmo -= ammoReload;
        }
    }
}
