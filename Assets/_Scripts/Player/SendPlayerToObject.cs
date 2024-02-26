using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class SendPlayerToObject : NetworkBehaviour
{
    public ServerPlayer sp;

    public void Update()
    {
        if (sp == null)
        {
            if (base.IsOwner)
            {
                sp = GameObject.Find("GameManager").GetComponent<ServerPlayer>();
                sp.player = this.gameObject;
                Debug.Log("Here!");
            }
        }
    }
}
