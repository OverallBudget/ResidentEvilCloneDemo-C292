using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    [SerializeField] protected int ammoCapacity;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected int currentSpareAmmo;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected bool canFire;

    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float damage;
    protected virtual void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    protected virtual void Fire()
    {

    }

    protected virtual void Reload()
    {

    }
}
