using FishNet.Example.ColliderRollbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class AssignCamera : NetworkBehaviour
{
    Camera playerCamera;
    [SerializeField]private float cameraYOffset = 0.4f;
    public void Update()
    {
        if(base.IsOwner)
        {
            playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + cameraYOffset, transform.position.z);
            playerCamera.transform.SetParent(transform);
        }
    }
}
