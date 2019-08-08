using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hoop : MonoBehaviour
{
    public TextMeshProUGUI text;

    public string player;

    private void Start()
    {
        text.SetText(PlayerPrefs.GetInt(player) + "");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            PlayerPrefs.SetInt(player, PlayerPrefs.GetInt(player) + 1);

            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");   
        }
    }
}
