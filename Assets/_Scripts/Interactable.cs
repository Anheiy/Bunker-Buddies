using FishNet.Managing.Server;
using FishNet.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder.MeshOperations;

public class Interactable : NetworkBehaviour
{
    public enum interactableType {Press,Hold}
    public ServerPlayer sp;
    public LookingAt LookingAt;
    public UnityEvent interactable;
    public interactableType InteractableType = interactableType.Press;
    public KeyCode interactableKey = KeyCode.None;
    public string toWhat = "";
    //Holding Essentials
    public float HoldTime;
    [SerializeField]private float timer = 0;
    private float degen;
    private bool isInvoked = false;
    void Update()
    {
        FindDependencies();
        LookAt();
    }
    private void LookAt()
    {
        if (LookingAt.distanceBetween <= 2.2 && LookingAt.objectViewed == this.gameObject)
        {
            LookingAt.pickupableText.text = InteractableType.ToString() + " " + interactableKey.ToString() + "  to " + toWhat;
            if (InteractableType == interactableType.Press)
            {
                if (Input.GetKeyDown(interactableKey))
                {
                    interactable.Invoke();
                }
            }
            else
            {
                //controls the timer
                if(Input.GetKey(interactableKey))
                {
                    if(timer < HoldTime + 0.1f)
                    timer += Time.deltaTime;
                }
                else
                {
                    if(timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                }
                //Invokes the Event
                if(timer >= HoldTime)
                {
                    if (isInvoked == false)
                    {
                        interactable.Invoke();
                        isInvoked = true;
                    }
                }
                else if(timer < HoldTime) isInvoked = false;
            }
        }
    }
    private void FindDependencies()
    {
        if (!sp)
        {
            Debug.Log("BING");
            sp = GameObject.Find("GameManager").GetComponent<ServerPlayer>();
        }
        if(!LookingAt)
        {
            LookingAt = sp.player.GetComponent<LookingAt>();
        }
    }
}
