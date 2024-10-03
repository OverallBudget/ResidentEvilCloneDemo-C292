using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour, IPickUpable
{
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private int ammoCount;
    [SerializeField] private int ammoCapacity;
    [SerializeField] private int currentAmmo;
    [SerializeField] private int reloadAmount;
    [SerializeField] private string ammoType;

    public int AmmoCount { get => ammoCount; set => ammoCount = value; }
    public int AmmoCapacity { get => ammoCapacity; set => ammoCapacity = value; }

    public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    public int ReloadAmount { get => reloadAmount; set => reloadAmount = value; }
    public string AmmoType { get => ammoType; set => ammoType = value; }


    public void OnDrop(Transform position)
    {
        gameObject.SetActive(true);
        transform.position = position.position;
        gameObject.transform.parent = null;
    }
    public void OnPickup(PlayerController player)
    {
        player.CurrentMag = this;
        gameObject.SetActive(false);
        gameObject.transform.parent = player.transform;
    }

    public void Reload()
    {
        if(ammoCapacity > 0)
        {
            int ammoReload = ammoCapacity - AmmoCount;

            if (ammoReload > currentAmmo)
            {
                Debug.Log("Reloaded " + currentAmmo);
                ammoCount += currentAmmo;
                currentAmmo = 0;
            }
            else
            {
                Debug.Log("Reloaded " + ammoReload);
                ammoCount = ammoCapacity;
                currentAmmo -= ammoReload;
            }
        }
    }
}
