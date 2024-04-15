using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : NetworkBehaviour
{
    private LookingAt lookingAt;
    private ServerPlayer sp;
    [Header("Functionality")]
    public Transform PlayerPos;
    private CinematicToggle cinematic;
    public GameObject termOff;
    public GameObject termOn;
    private bool InTerminal = false;
    [SyncVar] public bool isOccupied = false;

    private void Update()
    {
        FindDependencies();
        if (Input.GetKeyDown(KeyCode.Escape) && InTerminal == true)
        {
            DisableTerminal();
        }
            
    }
    public void ToggleTerminal()
    {
        if (!InTerminal)
        {
            EnableTerminal();
        }
    }
    public void EnableTerminal() 
    {
        if(isOccupied)
        {
            lookingAt.pickupableText.text = "Terminal Occupied";
        }
        else
        {
            lookingAt.pickupableText.text = "Press E to Toggle Terminal";
            if (Input.GetKeyDown(KeyCode.E))
            {
                sp.player.GetComponent<PlayerRotation>().enabled = false;
                sp.player.GetComponent<PlayerMovement>().enabled = false;
                sp.player.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 90f, 0f);
                Camera.main.transform.rotation = this.transform.rotation * Quaternion.Euler(0f, 90f, 0);
                sp.player.GetComponent<TransformInterprolator>().target = PlayerPos;
                cinematic.ToggleOff();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                InTerminal = true;
                lookingAt.pickupableText.text = "";
                termOn.SetActive(true);
                termOff.SetActive(false);
                isOccupiedOn();
            }
        }
    }
    public void DisableTerminal()
    {
        sp.player.GetComponent<PlayerRotation>().enabled = true;
        sp.player.GetComponent<PlayerMovement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InTerminal = false;
        sp.player.GetComponent<TransformInterprolator>().StopMoving();
        termOn.SetActive(false);
        termOff.SetActive(true);
        isOccupiedOff();
        cinematic.ToggleOn();
    }
    private void FindDependencies()
    {
        if (!sp)
        {
            sp = GameObject.Find("GameManager").GetComponent<ServerPlayer>();
        }
        if (!lookingAt)
        {
            lookingAt = sp.player.GetComponent<LookingAt>();
        }
        if(!cinematic)
        {
            cinematic = GameObject.Find("GameManager").GetComponent<CinematicToggle>();
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void isOccupiedOn()
    {
        isOccupied = true;
    }
    [ServerRpc(RequireOwnership = false)]
    private void isOccupiedOff()
    {
        isOccupied = false;
    }



}
