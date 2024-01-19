using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public LookingAt lookingAt;

    private void Update()
    {
        if (!lookingAt)
        {
            lookingAt = GameObject.Find("GameManager").GetComponent<ServerPlayer>().player.GetComponent<LookingAt>();
        }
        Inspect();
        
        
    }

    public void Inspect()
    {
        if (lookingAt.distanceBetween <= 2.2 && lookingAt.objectViewed == this.gameObject)
        {
            Debug.Log("in!");
            lookingAt.pickupableText.text = "Inspect " +this.name+ "? (E)";
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Clicked at " + lookingAt.distanceBetween);
                SetPipeState(false);
            }
        }
    }
    public void SetPipeState(bool status)
    {
        this.gameObject.SetActive(status);
    }
}
