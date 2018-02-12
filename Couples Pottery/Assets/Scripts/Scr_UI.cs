using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_UI : MonoBehaviour
{
    private static Scr_UI instance;
    private GameObject canvas;
    private GameObject title;
    private Text title_text;
    private Text turn_text;
    private int turns_left;
    private bool is_player_1;
    private bool at_title;
    private float input_timer = 0;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        at_title = true;
        title = transform.GetChild(1).gameObject;

        canvas = transform.GetChild(0).gameObject;
        title_text = canvas.transform.GetChild(2).GetComponent<Text>();
        turn_text = canvas.transform.GetChild(3).GetComponent<Text>();

        title.SetActive(true);
        //canvas.SetActive(false);
        is_player_1 = true;
        turns_left = 10;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (at_title)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                title.SetActive(false);
                canvas.SetActive(true);
                at_title = false;
            }
        }
        else
        {
            if (canvas.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Time.timeScale = 1;
                    canvas.SetActive(false);
                }
            }
        }
    }

    public static void ChangeSeat()
    {
        if (instance != null)
        {
            Time.timeScale = 0;
            instance.turns_left--;
            instance.canvas.SetActive(true);
            if (instance.is_player_1)
            {
                instance.title_text.text = "PLAYER TWO";
            }
            else
            {
                instance.title_text.text = "PLAYER ONE";
            }
            if (instance.turns_left > 10)
            {
                instance.turn_text.text = "turns left: " + instance.turns_left;
            }
            else
            {
                instance.turn_text.text = "turns left:  " + instance.turns_left;
                if(instance.turns_left == 0)
                {
                    instance.turn_text.text = "GAME OVER";
                }
            }

            instance.is_player_1 = !instance.is_player_1;
        }
    }
}
