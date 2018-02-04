using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour 
{

	// Use this for initialization
	public TextAsset csvFile; // Reference of CSV file

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter

    // Read data from CSV file
	// TODO: Make it so that we can request portions of the data.
    private string[] readData()
    {
		string[] easy,medium,hard,fuck;
        string[] records = csvFile.text.Split(lineSeperater);
        
		/*foreach (string record in records)
        {
            string[] fields = record.Split(fieldSeperator);
			if (fields[0] == "Easy")
			{
				easy = fields
			}

        }
		*/
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

