using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Generator : NetworkBehaviour
{
    private LookingAt LookingAt;
    private ServerPlayer sp;
    public Power power;
    private InventoryManager inventory;
    public float timePerCrank = 0.1f;
    public float crankPower = 0.1f;
    private bool canCrank = true;
    private float crankTimer = 0;
    private void Update()
    {
        FindDependencies();
        if(!canCrank)
        {
            crankTimer += Time.deltaTime; 
            if(crankTimer > timePerCrank)
            {
                crankTimer = 0;
                canCrank = true;
            }
        }
    }
    public void CrankGenerator()
    {
        LookingAt.pickupableText.text = "Crank Generator (E)";
        if(canCrank)
        if (Input.GetKey(KeyCode.E))
        {
            power.AddPower(crankPower);
                canCrank = false; 
        }
    }
    public void AddBattery()
    {
        LookingAt.pickupableText.text = "Needs a battery (E)";
        if (inventory.HeldItem.SendName() == "Battery")
        {
            LookingAt.pickupableText.text = "Insert Battery (E)";
            if (Input.GetKeyDown(KeyCode.E))
            {
                power.AddPower(100);
                inventory.RemoveItem(inventory.hotkey.currentlySelectedHotKey);
            }
        }
    }
    private void FindDependencies()
    {
        if (!sp)
        {
            sp = GameObject.Find("GameManager").GetComponent<ServerPlayer>();
        }
        if (!LookingAt)
        {
            LookingAt = sp.player.GetComponent<LookingAt>();
        }
        if(!inventory)
        {
            inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        }
    }


}
