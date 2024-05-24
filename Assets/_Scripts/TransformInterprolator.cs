using MonoFN.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransformInterprolator : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 1;
    private float sinTime;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 newPos = transform.position;
                newPos.x = Mathf.MoveTowards(this.transform.position.x, target.position.x, 2*Time.deltaTime);
                newPos.z = Mathf.MoveTowards(this.transform.position.z, target.position.z, 2*Time.deltaTime);
                transform.position = newPos;
                StartCoroutine(Moving());
            }
            
        }
    }

    IEnumerator Moving()
    {
        yield return new WaitForSeconds(1);
        target = null;
    }

    public void StopMoving()
    {
        StopAllCoroutines();
        target = null;
    }
}
