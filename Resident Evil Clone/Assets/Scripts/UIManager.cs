using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] TextMeshProUGUI ammoCount;
    [SerializeField] TextMeshProUGUI ammoSpare;
    [SerializeField] Pistol gun;
    private void Awake()
    {
        if(instance == null)
        {
        instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        try
        {
            gun = GameObject.Find("Pistol").GetComponent<Pistol>();
        }
        catch
        {
            Debug.LogWarning("No gun found.");
        }
    
    }

    private void Update()
    {
        updateAmmo();
    }

    public void updateAmmo()
    {
        if(gun.CurrentMag == null)
        {
            ammoCount.text = "0/0";
            ammoSpare.text = "0";
        }
        else
        {
            ammoCount.text = "" + gun.CurrentMag.AmmoCount + "/" + gun.CurrentMag.AmmoCapacity;
            ammoSpare.text = "" + gun.CurrentMag.CurrentAmmo;
        }
    }
}
