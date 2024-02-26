using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Rotate(0f, 1f * Time.deltaTime * speed, 0f, Space.Self);
    }
}
