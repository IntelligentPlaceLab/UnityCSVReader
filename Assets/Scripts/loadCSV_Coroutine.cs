using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadCSV_Coroutine : MonoBehaviour
{
    //make a list of sensors
    //TODO: fix to dictionary
    //Dictionary<string, Sensors> sensors = new Dictionary<string, Sensors>(); 
    List<Sensors> sensors = new List<Sensors>(); 
 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintCSV());
    }

    void update()
    {
        //StartCoroutine(PrintCSV());
    }

    IEnumerator PrintCSV() //IEnumerator is the data type coroutine returns
    {
         //load the csv
         //the csv needs to be in a folder called Resources
        TextAsset sensorData = Resources.Load<TextAsset>("report_29_05_22-02_06_22 (2)");

        //split by line and put it in a string array
        string[] data = sensorData.text.Split(new char[] { '\n' });

        //run through the line
        for(int i = 1; i < data.Length - 1; i++)
        {
            //split by comma and place in a string array
            //might not need the new char[] method and use only Split()
            string[] row = data[i].Split(new char[] {','}); 

            
            //check if name is valid and not empty
            //bug was in prevous code where if(row[i] ...) this is incorrect
            //correct code fixed below
            if(row[1] != "") //if the row isn't empty then do the loop
            {
                //initiate the Sensors class
                Sensors s = new Sensors();
                s.SensorID = row[0];
                s.Type = row[1];
                s.Location = row[2];
                s.Event = row[3];
                s.Time = row[4];
                s.Day = row[5];

                //add them to the list of sensors
                sensors.Add(s);
            }
        }

        foreach(Sensors s in sensors)
        {
            //print the list of sensors to console
            Debug.Log("ID: " + s.SensorID + "Type: " + s.Type + " Location: " + s.Location + " Even: " + s.Event + " Time: " + s.Time + " Day: " + s.Day);
            //return the data in inervals of seconds
            yield return new WaitForSeconds(1.0f); 
        }
    }
}
