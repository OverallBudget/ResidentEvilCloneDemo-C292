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
    
    }

    private void Update()
    {
        updateAmmo();
    }

    public void updateAmmo()
    {
        if(gun.currentMag == null)
        {
            ammoCount.text = "0/0";
            ammoSpare.text = "0";
        }
        else
        {
            ammoCount.text = "" + gun.currentMag.CurrentAmmo + "/" + gun.currentMag.AmmoCapacity;
            ammoSpare.text = "" + gun.currentMag.AmmoCount;
        }
    }
}
