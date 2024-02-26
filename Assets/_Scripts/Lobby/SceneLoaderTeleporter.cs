using FishNet;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderTeleporter : MonoBehaviour
{
    public Vector3 location;
    public float Time;
    public GameObject[] uiToDisable;
    public GameObject[] uiToEnable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerTeleport>().Teleport(location, Time);
            if (other.GetComponent<NetworkObject>().IsOwner)
            {
                Debug.Log("Here");
                foreach (GameObject ui in uiToDisable)
                {
                    ui.SetActive(false);
                }
                foreach (GameObject ui in uiToEnable)
                {
                    ui.SetActive(true);
                }
            }
        }
    }
    

    
}
