using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalMap : MonoBehaviour
{
    public Camera MoveableCam;
    private GameObject[] playerObjects;
    private int index = 0;
    void Start()
    {
        FindPlayers();
    }
    private void Update()
    {
        Debug.Log("index = " + index);
        if (this.isActiveAndEnabled) SetCamera();
    }

    public void MoveForward()
    {
        FindPlayers();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Forward");
            if (index < playerObjects.Length - 1)
                index++;
            else
                index = 0;
        }
    }
    public void MoveBackward()
    {
        FindPlayers();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (index > 0)
                index--;
            else
                index = playerObjects.Length - 1;
        }
    }

    public void SetCamera()
    {
        MoveableCam.transform.position = playerObjects[index].transform.position;
        MoveableCam.transform.rotation = playerObjects[index].transform.rotation;
    }
    public void FindPlayers()
    {
        playerObjects = GameObject.FindGameObjectsWithTag("Player");
    }
}
