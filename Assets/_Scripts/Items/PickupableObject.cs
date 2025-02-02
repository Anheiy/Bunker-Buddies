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
    }

    public void PickUp()
    {
            if (itemInformation.SendItemType() == "tool")
            {
                if (inventory.CheckInventory(itemInformation, this.GetComponent<Charge>().charge) == true)
                {
                    DespawnObjectServer(this.gameObject);
                }
            }
            else
            {
                if (inventory.CheckInventory(itemInformation) == true)
                {
                    DespawnObjectServer(this.gameObject);
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
            sp = GameObject.Find("GameManager").GetComponent<ServerPlayer>();
        }
        if(!LookingAt)
        {
            LookingAt = sp.player.GetComponent<LookingAt>();
        }
    }

}
