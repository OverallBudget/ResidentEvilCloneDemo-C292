using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour, IPickUpable
{
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private int ammoCount;
    [SerializeField] private int ammoCapacity;
    [SerializeField] private string ammoType;

    public int AmmoCount { get => ammoCount; set => ammoCount = value; }
    public int AmmoCapacity { get => ammoCapacity; set => ammoCapacity = value; }
    public string AmmoType { get => ammoType; set => ammoType = value; }

    public void OnDrop()
    {
        GameObject mag = Instantiate(ammoPrefab, transform.position, transform.rotation);
    }
    public void OnPickup(PlayerController player)
    {
        player.CurrentMag = this;
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
