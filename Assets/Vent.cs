using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    public Material openMat;
    public Material closeMat;
    public bool isOpen = true;
    private LookingAt LookingAt;
    private ServerPlayer sp;
    public Power power;
    public float timePerCloseTick = 0.1f;
    public float tickPower = 0.1f;
    private bool canTick = true;
    private float tickTimer = 0;
    private void Update()
    {
        FindDependencies();
        if (power.getPower() <= 0)
        {
            OpenVent();
        }
        //logic for vent power
        if (!isOpen)
            if (!canTick)
            {
                tickTimer += Time.deltaTime;
                if (tickTimer > timePerCloseTick)
                {
                    tickTimer = 0;
                    canTick = true;
                }
            }
            else
            {
                power.SubtractPower(0.3f);
                canTick = false;
            }
    }
    public void ChangeVent()
    {
        if (isOpen)
            LookingAt.pickupableText.text = "Close Vent (E)";
        else
            LookingAt.pickupableText.text = "Open Vent (E)";
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                CloseVent();
            }
            else
            {
                OpenVent();
            }
        }
    }
    private void CloseVent()
    {
        this.GetComponent<MeshRenderer>().material = openMat;
        isOpen = false;
    }
    private void OpenVent()
    {
        this.GetComponent<MeshRenderer>().material = closeMat;
        isOpen = true;
    }
    private void FindDependencies()
    {
        if (!sp)
        {
            sp = GameObject.Find("GameManager").GetComponent<ServerPlayer>();
        }
        if(!LookingAt)
        {
            LookingAt = sp.player.GetComponent<LookingAt>();
        }
    }
}
