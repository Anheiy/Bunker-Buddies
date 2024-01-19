using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FishNet.Object;
using FishNet.Connection;

public class PickupableObject : NetworkBehaviour
{
   
    public Item itemInformation;
    private ServerPlayer sp;
    private InventoryManager inventory; 
    private LookingAt LookingAt;


    void Update()
    {
        FindDependencies();
        PickUp();
    }

    private void PickUp()
    {
        if (LookingAt.distanceBetween <= 2.2 && LookingAt.objectViewed == this.gameObject)
        {
            LookingAt.pickupableText.text = "Pick Up " + itemInformation.SendName() + " (E)";
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Clicked at " + LookingAt.distanceBetween);
                if (inventory.CheckInventory(itemInformation) == true)
                {
                    DespawnObjectServer(this.gameObject);
                }
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    void DespawnObjectServer(GameObject objToDespawn)
    {
        ServerManager.Despawn(objToDespawn);
        Debug.Log("Destroyed");
    }
    private void FindDependencies()
    {
        if (!inventory)
        {
            inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        }
        if (!sp)
        {
            Debug.Log("BING");
            sp = GameObject.Find("GameManager").GetComponent<ServerPlayer>();
            LookingAt = sp.player.GetComponent<LookingAt>();
        }
    }

}
