using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour
{
    //Really, I should be using enums, but fuck it.
    //Level 0 = Easy
    //Level 1 = Medium
    //Level 2 = Hard
    //Level 3 = Fuck
    string[] easy, medium, hard, fuck;
    string[] records;
    int userChoice;
    Text promptText;
    CSVReader reader;
    // Use this for initialization
    void Start()
    {
        reader = transform.GetComponentInParent<CSVReader>();
        records = reader.readData();
		//givePrompt(1);
        //easy = records[0];
        //medium = records[1];
        //hard = records[2];
        //fuck = records[3];
    }

    void givePrompt(int level)
    {
        UnityEngine.Random rnd = new UnityEngine.Random();
        int choice;
        string temp;
        promptText = GameObject.Find("Prompt").GetComponent<Text>();

        switch (level)
        {
            case 0:
                temp = records[0];
                easy = temp.Split(',');
                choice = Random.Range(1, easy.Length);
                promptText.text = "your prompt " + easy[choice];
				print("beep");
                break;
            case 1:
                temp = records[1];
                medium = temp.Split(',');
                choice = Random.Range(1, medium.Length);
                promptText.text = "your prompt " + medium[choice];
                break;
            case 2:
                temp = records[2];
                hard = temp.Split(',');
                choice = Random.Range(1, hard.Length);
                promptText.text = "your prompt " + hard[choice];
                break;
            case 3:
                temp = records[3];
                fuck = temp.Split(',');
                choice = Random.Range(1, fuck.Length);
                promptText.text = "your prompt " + fuck[choice];
                break;
            default:
                print("Y'all gave the wrong value for level");
                break;
        }

    }
    
    public void onClick()
    {
        Dropdown menu;
        GameObject titleScreen, gameScreen;

        titleScreen = GameObject.Find("Title");
        gameScreen = GameObject.Find("Game");
        menu = GameObject.Find("Difficulty").GetComponent<Dropdown>();

        titleScreen.SetActive(false);
        //gameScreen.SetActive(true);

        userChoice = menu.value;

        givePrompt(userChoice);
    }
}
