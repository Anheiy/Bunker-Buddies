using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerminalInteractable : MonoBehaviour
{
    public ServerPlayer sp;
    public LookingAt LookingAt;
    public UnityEvent onUIClicked;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FindDependencies();
        ClickUI();
    }

    private void ClickUI()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        if (LookingAt.distanceBetween <= 2.2 && LookingAt.uiObj == this.gameObject)
        {
            onUIClicked.Invoke();
        }
    }
    private void FindDependencies()
    {
        if (!sp)
        {
            sp = GameObject.Find("GameManager").GetComponent<ServerPlayer>();
        }
        if (!LookingAt)
        {
            LookingAt = sp.player.GetComponent<LookingAt>();
        }
    }
}


