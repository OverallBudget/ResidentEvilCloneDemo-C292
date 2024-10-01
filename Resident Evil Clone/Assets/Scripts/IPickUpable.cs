using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUpable
{
    int AmmoCount { get; set; }
    int AmmoCapacity { get; set; }
    string AmmoType { get; set; }


    void OnPickup(PlayerController player);

    void OnDrop(Transform position);
}
    
