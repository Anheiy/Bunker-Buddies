using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    
    public void Teleport(Vector3 location, float time)
    {
        StartCoroutine(TimedTeleport(location, time));
        
    }
    IEnumerator TimedTeleport(Vector3 location, float time)
    {
        this.transform.position = new Vector3(-40.649f, -125.4245f, -1064f);
        yield return new WaitForSeconds(time);
        this.transform.position = location;
    }
}
