using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalCameras : MonoBehaviour
{
    public Camera MoveableCam;
    public GameObject[] cameraObjects;
    private int index = 0;
    private void Update()
    {
        Debug.Log("index = " +  index);
        if(this.isActiveAndEnabled) SetCamera();
    }

    public void MoveForward()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Forward");
            if(index < cameraObjects.Length - 1)
            index++;
            else
            index = 0;
        }
    }
    public void MoveBackward()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (index > 0)
                index--;
            else
                index = cameraObjects.Length - 1;
        }
    }

    public void SetCamera()
    {
        MoveableCam.transform.position = cameraObjects[index].transform.position;
        MoveableCam.transform.rotation = cameraObjects[index].transform.rotation;
    }
}
