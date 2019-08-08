using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > 9)
        {
            ScreenShake.shake = .075f;
        } else
        {
            ScreenShake.shake = 0;
        }
    }
}
