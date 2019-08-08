using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public static List<Rigidbody2D> rbs = new List<Rigidbody2D>();

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameObject.Find("Game Manager").transform;

        rbs = new List<Rigidbody2D>();

        foreach (Rigidbody2D rb in transform.parent.GetComponentsInChildren<Rigidbody2D>())
        {
            if (!rbs.Contains(rb))
            {
                rbs.Add(rb);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Bullet>())
        {
            if (collision.tag == "Good Guy" && collision.gameObject.name == "Good Guy" && Game_Manager.playing)
            {
                Game_Manager.playing = false;
                StartCoroutine(Game_Manager.EndGame(false));
            }
            else if (collision.tag == "Bad Guy" && collision.gameObject.name == "Bad Guy" && Game_Manager.playing)
            {
                Game_Manager.playing = false;
                StartCoroutine(Game_Manager.EndGame(true));
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Game_Manager.playing)
        {
            foreach (Rigidbody2D rb in rbs)
            {
                float dist = (transform.position - rb.transform.position).magnitude;

                //if ((transform.position - rb.transform.position).normalized * 1f / Mathf.Sqrt(dist) * 500f != )
                    rb.AddForce((transform.position - rb.transform.position).normalized * 1f / Mathf.Sqrt(dist) * 700f);
            }
        } 
    }
}
