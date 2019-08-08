using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float Shake = 0f;
    public static float shake = 0f;

    void Update()
    {
        if (Shake != 0)
            shake = Shake;

        if (shake > 0)
        {
            transform.localScale = new Vector3(1 - shake * .3f, 1 - shake * .3f, 1);
            transform.position = new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake), -10);
        }
    }
}
