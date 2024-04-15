using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalCameras : MonoBehaviour
{
    public enum CameraType { Bunker,Body, World}
    public CameraType cameraType = CameraType.Bunker;
    public Camera MoveableCam;
    public GameObject[] cameraObjects;
    public GameObject playerButtonsObject;
    private GameObject[] playerObjects;
    private int bunkerIndex = 0;
    private int playerIndex = 0;
    private void Start()
    {
        FindPlayers();
    }
    private void Update()
    {
        if(this.isActiveAndEnabled) SetCamera();
    }

    public void MoveForward()
    {
            if (cameraType == CameraType.Bunker)
            {
                if (bunkerIndex < cameraObjects.Length - 1)
                    bunkerIndex++;
                else
                    bunkerIndex = 0;
            }
            else
            {
                FindPlayers();
                if (playerIndex < playerObjects.Length - 1)
                    playerIndex++;
                else
                    playerIndex = 0;
            }
    }
    public void MoveBackward()
    {
            if (cameraType == CameraType.Bunker)
            {
                if (bunkerIndex > 0)
                    bunkerIndex--;
                else
                    bunkerIndex = cameraObjects.Length - 1;
            }
            else
            {
                FindPlayers();
                if (playerIndex > 0)
                    playerIndex--;
                else
                    playerIndex = playerObjects.Length - 1;
            }
    }

    public void SetCamera()
    {
        if (cameraType == CameraType.Bunker)
        {
            MoveableCam.transform.position = cameraObjects[bunkerIndex].transform.position;
            MoveableCam.transform.rotation = cameraObjects[bunkerIndex].transform.rotation;
        }
        else if(cameraType == CameraType.Body)
        {
            MoveableCam.transform.position = playerObjects[playerIndex].transform.position;
            MoveableCam.transform.rotation = playerObjects[playerIndex].transform.rotation;
        }
        else
        {
            MoveableCam.transform.position = new Vector3 (playerObjects[playerIndex].transform.position.x, playerObjects[playerIndex].transform.position.y + 20, playerObjects[playerIndex].transform.position.z) ;
            MoveableCam.transform.LookAt(playerObjects[playerIndex].transform);
        }
    }
    
    public void FindPlayers()
    {
        playerObjects = GameObject.FindGameObjectsWithTag("Player");
    }
    public void BunkerButton()
    {
        cameraType = CameraType.Bunker;
        playerButtonsObject.SetActive(false);
    }
    public void PlayerButton()
    {
        FindPlayers();
        cameraType = CameraType.Body;
        playerButtonsObject.SetActive(true);
    }
    public void WorldButton()
    {
        cameraType = CameraType.World;
    }
    public void BodyButton()
    {
        cameraType = CameraType.Body;
    }
}
