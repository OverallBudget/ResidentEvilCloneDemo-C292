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

        if (canFire && currentMag.AmmoCount > 0)
        {
            //Debug.Log("Pistol Fired");
            currentMag.AmmoCount--;
            RaycastHit hit;
            float range = 100f;
            Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 2f);

            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100))
            {
                
                if (hit.transform.CompareTag("Zombie"))
                {
                    hit.transform.GetComponent<Zombie>().TakeDamage(damage);
                }
            }   
        }
        
        if(currentMag.AmmoCapacity <= 0)
        {
            Debug.Log("Current Mag is empty.");
        }

        if(currentMag.AmmoCount == 0 && canFire)
        {
            Debug.Log("Reloading...");
            Reload();
            Debug.Log("Out of Ammo.");
        }
    }

    protected override void Reload()
    {
        //int ammoReload = currentMag.AmmoCapacity - currentMag.CurrentAmmo;
        //if (ammoReload == 0)
        //{
        //    Debug.Log("Pistol already full.");
        //}
        //else if (currentMag.CurrentAmmo > 0)
        //{
        //    Reload();
        //}
        //else
        //{
        //    Debug.Log("Out of Ammo.");
        //} 
        StartCoroutine(ReloadCoroutine()); // keep things simple I guess.
    }

    private IEnumerator ReloadCoroutine()
    {
        if(currentMag.CurrentAmmo == 0)
        {
            Debug.Log("Out of Ammo.");
            yield break;
        }

        canFire = false;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        canFire = true;
        currentMag.Reload();
        Debug.Log("Pistol Reloaded");
        //int ammoReload = currentMag.AmmoCapacity - currentMag.CurrentAmmo;

    }
}
