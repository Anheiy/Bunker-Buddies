using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class DropItem : NetworkBehaviour
{
    InventoryManager inventory;
    HotkeyManager hotkey;
    // Start is called before the first frame update
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindDependencies();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(hotkey.currentlySelectedHotKey > -1 && inventory.slotlist[hotkey.currentlySelectedHotKey + 1] != null)
            {
                DropItemOnServer(inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendObject(), this.gameObject.transform);
                inventory.RemoveItem(hotkey.currentlySelectedHotKey + 1);
            }
        }
    }
    [ServerRpc]
    public void DropItemOnServer(NetworkObject obj, Transform player)
    {
        NetworkObject spawned = NetworkManager.GetPooledInstantiated(obj, player.position + player.forward, Quaternion.identity, true);
        ServerManager.Spawn(spawned);
    }
    public void FindDependencies()
    {
        if (!inventory)
        {
            inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        }
        if (!hotkey)
        {
            hotkey = GameObject.Find("GameManager").GetComponent<HotkeyManager>();
        }
    }
}
