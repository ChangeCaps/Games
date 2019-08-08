using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    string[] input;

    bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
            grounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
            grounded = false;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ball" && Input.GetKey(input[4]))
        {
            if (grounded)
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 8);
            } else
            {
                collision.GetComponent<Rigidbody2D>().velocity = (collision.transform.position - transform.position).normalized * 12;
            }
        } 
    }

    private void FixedUpdate()
    {
        Vector2 vel = new Vector2();
        vel.y = rb.velocity.y;

        if (Input.GetKey(input[4]))
            GetComponent<Animator>().Play("Expand");
        else
        {
            if (Input.GetKey(input[1]))
                vel.x += 5;

            if (Input.GetKey(input[3]))
                vel.x -= 5;

            if (Input.GetKey(input[0]) && grounded)
                vel.y = 7;
        }

        if (vel.x > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (vel.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;

        rb.velocity = vel;
    }
}
