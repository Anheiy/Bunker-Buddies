using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    [SerializeField] private float power = 100;
    [SerializeField] private float powerWeight = 1;
    [SerializeField] private Image powerImage;
    private void Start()
    {
        InvokeRepeating("PowerDegen", 0, 1);
    }
    private void Update()
    {
        powerImage.fillAmount = power / 100;
        if(power > 100)
        {
            power = 100;
        }
        if (power < 0)
        {
            power = 0;
        }
    }
    public void AddPower(float addedPower)
    {
        power = power + addedPower;
    }
    public void SubtractPower(float subtractedPower)
    {
        power = power - subtractedPower;
    }
    private void PowerDegen()
    {
        SubtractPower(powerWeight);
    }
    public float getPower()
    {
        return power;
    }

}
