using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodGuy : MonoBehaviour
{
    public float speed;

    Animator an;

    int dash = 0;

    Vector3 d;

    public Collider2D trigger;
    public Collider2D trigger2;

    public static Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        an = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bad Guy" && Game_Manager.playing)
        {
            Game_Manager.playing = false;
            StartCoroutine(Game_Manager.EndGame(true));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t = transform;

        if (dash > 0)
            dash--;

        if (dash < 15)
        {
            trigger.enabled = false;
            trigger2.enabled = false;
            an.SetBool("Dash", false);

            Vector3 mov = new Vector3();

            mov.x += Input.GetAxis("Horizontal");
            mov.y += Input.GetAxis("Vertical");

            if (mov.y > .8f)
            {
                an.SetInteger("Direction", 3);
            }
            else if (mov.y < -.8f)
            {
                an.SetInteger("Direction", 1);
            }
            else if (mov.x > .5f)
            {
                an.SetInteger("Direction", 2);
            }
            else if (mov.x < -.5f)
            {
                an.SetInteger("Direction", 4);
            }
            else
            {
                an.SetInteger("Direction", 0);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                trigger.enabled = true;
                trigger2.enabled = true;

                GetComponent<AudioSource>().Play();

                an.SetBool("Dash", true);
                dash = (int)(60 * .5f) + 5;
                d = mov;
            }

            GetComponent<Rigidbody2D>().velocity = (mov.normalized * speed * 90);
        } else
        {
            GetComponent<Rigidbody2D>().velocity = (d.normalized * speed * 1.7f * 90);
        }
    }
}
