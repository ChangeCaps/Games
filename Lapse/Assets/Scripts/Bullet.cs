using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool moving = true;

    private void Start()
    {
        BlackHole.rbs.Add(GetComponent<Rigidbody2D>());

        transform.parent = GameObject.Find("Game Manager").transform;
    }

    void FixedUpdate()
    {
        if (moving)
            GetComponent<Rigidbody2D>().velocity = -transform.right*.15f*90;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Destroy Bullets")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3();

            moving = false;

            GetComponent<Animator>().Play("Bullet Break");

            BlackHole.rbs.Remove(GetComponent<Rigidbody2D>());

            enabled = false;

            Invoke("kill", .05f);
        } else if (collision.tag == "Good Guy" && collision.gameObject.name == "Good Guy" && Game_Manager.playing && collision.transform.parent.GetComponent<GoodGuy>())
        {
            Game_Manager.playing = false;
            StartCoroutine(Game_Manager.EndGame(false));
        }
    }

    void kill()
    {
        gameObject.SetActive(false);
    }
}
