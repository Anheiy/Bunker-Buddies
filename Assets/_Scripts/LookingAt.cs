using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Object.Synchronizing;
using FishNet.Demo.AdditiveScenes;
using Unity.VisualScripting;
using TMPro;

public class LookingAt : NetworkBehaviour
{
    private Camera playerCam;
    public GameObject objectViewed;
    public float distanceBetween;
    public TextMeshProUGUI pickupableText; 

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            base.GetComponent<LookingAt>().enabled = false;
        }
    }
    private void Start()
    {
        playerCam = Camera.main;
        pickupableText = GameObject.Find("PickupText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        ShootRay();
        EnablePickup();
    }
    public void ShootRay()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            this.objectViewed = hit.transform.gameObject;
            Debug.DrawRay(ray.origin, hit.point * 10, Color.yellow);
        }
        if(objectViewed != null) 
        distanceBetween = Vector3.Distance(this.gameObject.transform.position, objectViewed.transform.position);
    }
    public void EnablePickup()
    {
        //rework this system make it go off a object tag = interactable
        if (distanceBetween <= 2.2 && objectViewed.GetComponent<PickupableObject>() != null || objectViewed.GetComponent<Pipe>() != null)
        {
            pickupableText.enabled = true;
        }
        else
        {
            pickupableText.enabled = false;
        }
    }
}
