using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour 
{

	// Use this for initialization
	public TextAsset csvFile; // Reference of CSV file

    private char lineSeparator = '\n'; // It defines line seperate character
    private char fieldSeparator = ','; // It defines field seperate chracter

    // Read data from CSV file
	// TODO: Make it so that we can request portions of the data.
    public string[] readData()
    {
        string[][] container;
		string[] easy,medium,hard,fuck;
        string temp;
        string[] records = csvFile.text.Split(lineSeparator);
        for(int i=0; i<records.Length; i++)
        {
            temp = records[i];
            if (i == 0)
            {
                easy = temp.Split(fieldSeparator);
                print(easy);
            }
            if (i == 1)
            {
                medium = temp.Split(fieldSeparator);
            }
            if (i == 2)
            {
                hard = temp.Split(fieldSeparator);
            }
            if (i == 3)
            {
                fuck = temp.Split(fieldSeparator);
            }
        }
        //print(records[0]);

		return records;
    }
    // Add data to CSV file
 

    /* Get path for given CSV file
    private static string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath;

    }
	*/

}

