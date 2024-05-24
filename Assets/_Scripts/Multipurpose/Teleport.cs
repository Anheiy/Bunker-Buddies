using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 TeleportPosition;
    public float TimeBeforeTeleport = 0;
    public enum TeleportOption { OnEnter, OnExit, OnInteract }
    public TeleportOption teleportOption = TeleportOption.OnInteract;
    public string LocationName;
    private ServerPlayer sp;
    private LookingAt LookingAt;

    private void Start()
    {
        sp = GameObject.Find("GameManager").GetComponent<ServerPlayer>();
        LookingAt = sp.player.GetComponent<LookingAt>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (teleportOption == TeleportOption.OnEnter)
            {
                StartCoroutine(TeleportToLocation());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (teleportOption == TeleportOption.OnExit)
            if (other.tag == "Player")
            {
                StartCoroutine(TeleportToLocation());
            }
    }
    public void InteractableTeleport()
    {
            StartCoroutine(TeleportToLocation());
    }

    IEnumerator TeleportToLocation()
    {
        yield return new WaitForSeconds(TimeBeforeTeleport);
        sp.player.transform.position = TeleportPosition;
    }

}
