using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuy : MonoBehaviour
{
    Animator an;
    Rigidbody2D rb;

    AudioSource audio;

    Vector3 target = new Vector3(0, 2, 0);

    Vector3 t = new Vector3();

    int fire = 4;
    bool right = true;

    public Transform bullet;

    int cooldown = 0;
    int dash_cooldown = 1000;

    int time = 0;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();

        if (Game_Manager.level == 0 || Game_Manager.level == 9)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Destroy Bullets")
        {
            target = transform.position - (transform.position - GoodGuy.t.position).normalized * 4;
            time = 30;
            dash_cooldown = 0;

            an.Play("Idle");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time > 0)
            time--;

        if ((target - transform.position).magnitude > .1f && time > 0)
        {
            if (dash_cooldown == 0)
            {
                if (Game_Manager.level == 1)
                {
                    fire = 3;
                }

                if (fire > 0)
                    fire--;

                if ((GoodGuy.t.position - transform.position).normalized.y > .8f)
                {
                    an.SetInteger("Dash Direction", 1);

                    if (right && fire == 0)
                    {
                        Transform t = Instantiate(bullet);
                        t.position = transform.position;
                        t.Translate(.1f, 0, 0);

                        t.right = this.t;

                        right = false;
                        fire = 3;

                        audio.Play();
                    }
                    else if (fire == 0)
                    {
                        Transform t = Instantiate(bullet);
                        t.position = transform.position;
                        t.Translate(-.1f, 0, 0);

                        t.right = this.t;

                        right = true;
                        fire = 3;

                        audio.Play();
                    }
                }
                else if ((GoodGuy.t.position - transform.position).normalized.y < -.8f)
                {
                    an.SetInteger("Dash Direction", 3);

                    if (right && fire == 0)
                    {
                        Transform t = Instantiate(bullet);
                        t.position = transform.position;
                        t.Translate(.1f, 0, 0);

                        t.right = this.t;

                        right = false;
                        fire = 3;

                        audio.Play();
                    }
                    else if (fire == 0)
                    {
                        Transform t = Instantiate(bullet);
                        t.position = transform.position;
                        t.Translate(-.1f, 0, 0);

                        t.right = this.t;

                        right = true;
                        fire = 3;

                        audio.Play();
                    }
                }
                else if ((GoodGuy.t.position - transform.position).normalized.x > 0)
                {
                    an.SetInteger("Dash Direction", 2);

                    GetComponent<SpriteRenderer>().flipX = true;

                    if (right && fire == 0)
                    {
                        Transform t = Instantiate(bullet);
                        t.position = transform.position;
                        t.Translate(0, .1f, 0);

                        t.right = this.t;

                        right = false;
                        fire = 3;

                        audio.Play();
                    }
                    else if (fire == 0)
                    {
                        Transform t = Instantiate(bullet);
                        t.position = transform.position;
                        t.Translate(0, -.1f, 0);

                        t.right = this.t;

                        right = true;
                        fire = 3;

                        audio.Play();
                    }
                }
                else
                {
                    an.SetInteger("Dash Direction", 4);

                    GetComponent<SpriteRenderer>().flipX = false;

                    if (right && fire == 0)
                    {
                        Transform t = Instantiate(bullet);
                        t.position = transform.position;
                        t.Translate(0, .1f, 0);

                        t.right = this.t;

                        right = false;
                        fire = 3;

                        audio.Play();
                    }
                    else if (fire == 0)
                    {
                        Transform t = Instantiate(bullet);
                        t.position = transform.position;
                        t.Translate(0, -.1f, 0);

                        t.right = this.t;

                        right = true;
                        fire = 3;

                        audio.Play();
                    }
                }
            }

            if ((target - transform.position).normalized.y > .8f)
            {
                an.SetInteger("Direction", 3);
            } else if ((target - transform.position).normalized.y < -.8f)
            {
                an.SetInteger("Direction", 1);
            } else if ((target - transform.position).normalized.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                an.SetInteger("Direction", 2);
            } else
            {
                GetComponent<SpriteRenderer>().flipX = true;
                an.SetInteger("Direction", 4);
            }

            if (dash_cooldown == 0)
            {
                GetComponent<Rigidbody2D>().velocity = ((target - transform.position).normalized * .2f * 90);
            } else
            {
                GetComponent<Rigidbody2D>().velocity = ((target - transform.position).normalized * .11f * 90);
            }
        } else
        {
            if (cooldown > 0)
                cooldown--;

            if (cooldown == -1)
                cooldown = 15;

            an.SetInteger("Dash Direction", 0);
            an.SetInteger("Direction", 0);

            if (cooldown == 0)
            {
                t = (transform.position - GoodGuy.t.position).normalized * .2f;

                cooldown = -1;

                dash_cooldown--;
                
                if ((GoodGuy.t.position - transform.position).magnitude < 4)
                {
                    if (dash_cooldown == 0)
                    {
                        target = transform.position + (transform.position - GoodGuy.t.position).normalized * 4;
                        time = 30;
                    }
                    else
                    {
                        target = transform.position + (transform.position - GoodGuy.t.position).normalized * 1.5f;
                        time = 30;
                    }
                }
                else if ((GoodGuy.t.position - transform.position).magnitude > 6)
                {
                    if (dash_cooldown == 0)
                    {
                        target = transform.position - (transform.position - GoodGuy.t.position).normalized * 4;
                        time = 30;
                    }
                    else
                    {
                        target = transform.position - (transform.position - GoodGuy.t.position).normalized * 1.5f;
                        time = 30;
                    }
                }
                else
                {
                    an.SetInteger("Direction", 0);

                    dash_cooldown = 0;

                    if (Random.Range(0f, 1f) > .5f)
                    {
                        target = transform.position - new Vector3((transform.position - GoodGuy.t.position).normalized.y, (transform.position - GoodGuy.t.position).normalized.x, (transform.position - GoodGuy.t.position).normalized.z) * 4;
                        time = 30;
                    } else
                    {
                        target = transform.position + new Vector3((transform.position - GoodGuy.t.position).normalized.y, (transform.position - GoodGuy.t.position).normalized.x, (transform.position - GoodGuy.t.position).normalized.z) * 4;
                        time = 30;
                    }
                }

                if (dash_cooldown == -1)
                    dash_cooldown = 2;
            }
        }
    }
}
