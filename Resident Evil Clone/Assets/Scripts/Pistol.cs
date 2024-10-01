using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Pistol : Weapon
{

    private void Start()
    {
        canFire = true; 
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Fire()
    {
        if (currentMag == null)
        {
            Debug.Log("No mag");
            return;
        }
        if (canFire)
        {
            if (currentMag.AmmoCount > 0)
            {
                //Debug.Log("Pistol Fired");
                currentMag.AmmoCount--;
                RaycastHit hit;
                if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100))
                {
                    Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
                    if (hit.transform.CompareTag("Zombie"))
                    {
                        hit.transform.GetComponent<Zombie>().TakeDamage(damage);
                    }
                }
                else
                {
                    Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
                }
            }
            else
            {
                if (currentMag.CurrentAmmo > 0)
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
        int ammoReload = currentMag.AmmoCapacity - currentMag.CurrentAmmo;
        if (ammoReload == 0)
        {
            Debug.Log("Pistol already full.");
        }
        else if (currentMag.CurrentAmmo > 0)
        {
            Reload();
        }
        else
        {
            Debug.Log("Out of Ammo.");
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        canFire = false;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        canFire = true;

        int ammoReload = currentMag.AmmoCapacity - currentMag.CurrentAmmo;

        currentMag.Reload();
    }
}
