using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotkeyManager : MonoBehaviour
{
    public Image[] Slots;
    public TextMeshProUGUI[] SlotNames;
    public Image[] selected;
    public int currentlySelectedHotKey;
    private Vector2 baseSize;
    private KeyCode[] keyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
    };
    
    void Start()
    {
        baseSize = Slots[0].rectTransform.sizeDelta;
        currentlySelectedHotKey = -1;
    }
    
    // Update is called once per frame
    void Update()
    {
        PressKey();
        ScrolltoSelect();
    }
    private void PressKey()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                if (currentlySelectedHotKey != i)
                {
                    Select(i);
                }
                else
                {
                    Deselect();
                }
            }

        }
    }
    public void ScrolltoSelect()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (currentlySelectedHotKey < Slots.Length - 1)
            {
                Select(currentlySelectedHotKey + 1);
            }
            else
            {
                Select(0);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentlySelectedHotKey > 0)
            {
                Select(currentlySelectedHotKey - 1);
            }
            else
            {
                Select(Slots.Length - 1);
            }
        }
        

    }
    private void ClearSize()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].rectTransform.sizeDelta = baseSize;
            selected[i].gameObject.SetActive(false);
            SlotNames[i].gameObject.SetActive(false);
        }
    }
    public void Select(int i)
    {
        ClearSize();
        Debug.Log(i);
        Slots[i].rectTransform.sizeDelta = new Vector2(75f, 75f);
        currentlySelectedHotKey = i;
        selected[i].gameObject.SetActive(true);
        SlotNames[i].gameObject.SetActive(true);
    }
    public void Deselect()
    {
        ClearSize();
        currentlySelectedHotKey = -1;
    }

    
}
