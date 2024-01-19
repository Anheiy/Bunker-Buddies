using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithPlayer : MonoBehaviour
{
    private GameObject mainCamera;
    private void Update()
    {
        if(!mainCamera)
        {
            mainCamera = GameObject.Find("Main Camera");
        }
        this.transform.rotation = mainCamera.transform.rotation;
    }
}
