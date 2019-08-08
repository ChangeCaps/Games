using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    
    void Update()
    {
        Vector3 v = GoodGuy.t.position;
        v.z = -10;

        transform.position = v;
    }
}
