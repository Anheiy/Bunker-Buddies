using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class DamageOnCollide : NetworkBehaviour
{
    public int damage;
    public bool shouldDestroyOnHit = true;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            if (shouldDestroyOnHit)
            {
                Despawn(this.gameObject);
            }
            other.GetComponent<Health>().modifyHealth(-damage,0,1,0);
        }
    }
}
