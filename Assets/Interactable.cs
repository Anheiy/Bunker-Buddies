using FishNet.Managing.Server;
using FishNet.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : NetworkBehaviour
{
    public ServerPlayer sp;
    public LookingAt LookingAt;
    public UnityEvent interactable;

    void Update()
    {
        FindDependencies();
        LookAt();
    }
    private void LookAt()
    {
        if (LookingAt.distanceBetween <= 2.2 && LookingAt.objectViewed == this.gameObject)
        {
            interactable.Invoke();
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
