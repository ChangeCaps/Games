using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    public static int level = 0;

    Transform[] spawnpoints;

    public List<GameObject> enables = new List<GameObject>();

    public static bool playing = false;

    public Transform[] spawnables;

    public List<GameObject> Enable = new List<GameObject>();
    static List<GameObject> enable = new List<GameObject>();

    public List<MonoBehaviour> Disable = new List<MonoBehaviour>();
    static List<MonoBehaviour> disable = new List<MonoBehaviour>();

    public List<Animator> D = new List<Animator>();
    static List<Animator> d = new List<Animator>();

    public Transform[] C = new Transform[2];
    static Transform[] c = new Transform[2];

    public TextMeshProUGUI[] Text = new TextMeshProUGUI[2];
    static TextMeshProUGUI[] text = new TextMeshProUGUI[2];

    static string[][] dialog =
    {
        new string[]
        {
            "2There is no point to hitting me!"
        },
        new string[]
        {
            "1Stop turning back time!",
            "1You know aswell as I that time travel causes anomalies!"
        },
        new string[]
        {
            "2Yet you do it yourself!",
            "1Yhea, because you force me to!"
        },
        new string[]
        {
            "2You could also just stop!",
            "1..."
        },
        new string[]
        {
            "1You got us here in the first place!",
            "2Yes, and i don't intend to let you leave!"
        },
        new string[]
        {
            "1Stop!",
            "1Turning",
            "1Back",
            "1Time                                 ",
            "2Why?"
        },
        new string[]
        {
            "1Look at the mess you've made!",
            "2You're contributing just as much as I am!"
        },
        new string[]
        {
            "1Why are you doing this?",
            "2Why are you?"
        },
        new string[]
        {
            "1I have no choise",
            "1You put us in here"
        },
        new string[]
        {
            "2Welp, i tried",
            "2You can't be stopped"
        },
        new string[]
        {
            "1What do you mean?",
        },
        new string[]
        {
            "2If we don't fight, we don't need to time travel"
        },
        new string[]
        {
            "2But i guess you couldn't see that",
            "1Hmpf..."
        },
        new string[]
        {

        },
        new string[]
        {

        }
    };

    public AudioClip A;
    static AudioClip a;

    static Transform t;

    public static AudioSource audio;

    private void Start()
    {
        spawnpoints = GameObject.Find("Spawn Points").GetComponentsInChildren<Transform>();

        playing = true;

        GetComponent<Animator>().enabled = false;

        audio = GetComponent<AudioSource>();
        enable = Enable;
        t = transform;
        disable = Disable;
        d = D;
        a = A;
        c = C;
        text = Text;

        foreach (var g in disable)
        {
            g.enabled = true;
        }

        foreach (var g in d)
        {
            g.enabled = true;
        }

        if (level != 9)
        {
            for (int i = 0; i < Mathf.Floor(level / 4); i++)
            {
                float r = Random.Range(0, 3);

                if (r < 1)
                {
                    var t = Instantiate(spawnables[Mathf.FloorToInt(Random.Range(0, spawnables.Length))]);

                    t.position = spawnpoints[Mathf.FloorToInt(Random.Range(0, spawnpoints.Length))].position;

                } else if (r < 2 && enables.Count > 0)
                {
                    int e = Mathf.FloorToInt(Random.Range(0, enables.Count));

                    enables[e].SetActive(true);
                    enables.RemoveAt(e);
                } else
                {

                }
            }
        }    
    }

    public static IEnumerator EndGame(bool won)
    {
        foreach (Bullet rb in t.GetComponentsInChildren<Bullet>())
        {
            rb.enabled = false;
            rb.GetComponent<Animator>().enabled = false;
        }

        foreach (var g in enable)
        {
            g.SetActive(!g.activeSelf);
        }

        foreach (var g in disable)
        {
            g.enabled = false;
        }

        foreach (var g in d)
        {
            g.enabled = false;
        }

        audio.Play();

        yield return new WaitForSeconds(.001f);

        foreach (Bullet rb in t.GetComponentsInChildren<Bullet>())
        {
            rb.enabled = false;
        }

        yield return new WaitForSeconds(.75f);

        if (level < dialog.Length)
        {
            for (int i = 0; i < dialog[level].Length; i++)
            {
                if (dialog[level][i][0] == '1')
                {
                    text[0].SetText(dialog[level][i].Substring(1));

                    audio.Play();

                    yield return new WaitForSeconds(dialog[level][i].Substring(1).Length * .035f + .4f);

                    if (i < dialog[level].Length - 1)
                        text[0].SetText("");
                }
                else
                {
                    text[1].SetText(dialog[level][i].Substring(1));

                    audio.Play();

                    yield return new WaitForSeconds(dialog[level][i].Substring(1).Length * .035f + .4f);

                    if (i < dialog[level].Length - 1)
                        text[1].SetText("");
                }
            }
        }

        audio.PlayOneShot(a);

        if (won)
        {
            c[1].gameObject.SetActive(true);
        } else
        {
            c[0].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1.5f);

        level++;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

        yield break;
    }
}
