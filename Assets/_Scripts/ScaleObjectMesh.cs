using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Update()
    {
        normalizeassetx(this.gameObject);
        
    }
    public static void normalizeassetx(GameObject asset)
    {
        var rend = asset.GetComponent<Renderer>();
        Bounds bound = rend.bounds;
        //Debug.Log(bound);
        float ma;
        float sizer = 10;
        if (bound.max.x >= bound.max.y) ma = bound.max.x;
        else ma = bound.max.y;
        ma = ma / sizer;
        if (ma < 0) ma *= -1;
        asset.transform.localScale = new Vector3(1 / ma, 1 / ma, 1 / ma);
    }
}
