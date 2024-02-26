using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerAttack : NetworkBehaviour
{
    bool canAttack = true;
    Camera playerCam;
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            base.GetComponent<PlayerAttack>().enabled = false;
        }
    }
    private void Update()
    {

    }
    private void Start()
    {
        playerCam = Camera.main;
    }
    public void AttackRay(float damage, float cooldown, float attackDistance, float layerValue)
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Attack(ray, damage, cooldown, attackDistance, layerValue);
    }
    [ServerRpc]
    public void Attack(Ray ray,float damage,float cooldown,float attackDistance, float layerValue)
    { 
        if (canAttack == true)
        {
            Debug.Log("Attack Attempt");
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Object Hit: " + hit.transform.gameObject);
                Debug.Log("Object Layer: " + hit.transform.gameObject.layer);
                Debug.Log("Layer to Hit: " + layerValue);
                Debug.DrawLine(ray.origin, hit.point, Color.green, 3);
                if (hit.transform.gameObject.layer == layerValue)
                {
                    float currentDis = Vector3.Distance(this.gameObject.transform.position, hit.transform.gameObject.transform.position);
                    if (attackDistance >= currentDis)
                    {
                        canAttack = false;
                        Health entityHealth = hit.transform.gameObject.GetComponent<Health>();
                        Debug.Log(this.name + " attacked " + entityHealth.name);
                        entityHealth.modifyHealth(-damage, 0, 1, 0);
                        
                        StartCoroutine(ResetAttackCooldown(cooldown));
                    }
                    else
                    {
                        Debug.Log("Not Close Enough... " + currentDis + " Away");
                    }
                }
            }
        }
        
    }
    IEnumerator ResetAttackCooldown(float cooldown)
    {
        Debug.Log("Attacking");
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}
