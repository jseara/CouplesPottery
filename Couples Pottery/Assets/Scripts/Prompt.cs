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
	Text promptText;
	CSVReader reader;
	// Use this for initialization
	void Start () 
	{
		reader = transform.GetComponentInParent<CSVReader>();
		string[] records = reader.readData();
		//easy = records[0];
		//medium = records[1];
		//hard = records[2];
		//fuck = records[3];
	}
	
	void givePrompt(int level)
	{
		UnityEngine.Random rnd = new UnityEngine.Random();
		int choice;
		switch (level)
		{
			case 0:
				choice = Random.Range(0, easy.Length);
				promptText = transform.GetComponentInParent<Text>();
				promptText.text = "Your prompt is: " + easy[choice];
				break;
			case 1:
				choice = Random.Range(0, medium.Length);
				break;
			case 2:
				choice = Random.Range(0, hard.Length);
				break;
			case 3:
				choice = Random.Range(0, fuck.Length);
				break;
			default:
				print("Y'all gave the wrong value for level");
				break;
		}
			
	}
	// Update is called once per frame
	void Update () 
	{
		
	}
}
