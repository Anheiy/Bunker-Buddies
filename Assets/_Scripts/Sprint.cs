using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    private PlayerMovement pm;
    private Stamina stamina;
    public float staminaToSprint = 30;


    // Update is called once per frame
    void Update()
    {
        FindDependencies();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina.stamina > staminaToSprint)
            {
                pm.currentMovementSpeed = pm.runSpeed;
            }
            else if (stamina.stamina == 0)
            {
                pm.currentMovementSpeed = pm.walkSpeed;
            }

            if (stamina.stamina != 0 && pm.currentMovementSpeed == pm.runSpeed)
            {
                stamina.stamina -= Time.deltaTime * 10;
            }
        }
        else
        {
            pm.currentMovementSpeed = pm.walkSpeed;
        }
    }

    public void FindDependencies()
    {
        if (!pm)
        {
            pm = this.GetComponent<PlayerMovement>();
        }
        if (!stamina)
        {
            stamina = this.GetComponent<Stamina>();
        }
    }
}
