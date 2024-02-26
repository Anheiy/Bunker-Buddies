using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class PlayerRotation : NetworkBehaviour
{
    public Vector2 sens;
    private Vector2 rotation;
    private GameObject mainCam;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            this.GetComponent<PlayerRotation>().enabled = false;
        }
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainCam = Camera.main.gameObject;

    }
    public void Update()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens.x;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens.y;

        rotation.y += mouseX;
        rotation.x -= mouseY;
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
        //Camera Rotation
        mainCam.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        //Player Rotation
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
    }
}
