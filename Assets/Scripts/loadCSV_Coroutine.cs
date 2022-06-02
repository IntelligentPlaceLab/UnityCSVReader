using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadCSV_Coroutine : MonoBehaviour
{
    //make a list of sensors
    //Dictionary<string, Sensors> sensors = new Dictionary<string, Sensors>(); 
    List<Sensors> sensors = new List<Sensors>(); 
 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintCSV());
    }

    IEnumerator PrintCSV() //IEnumerator is the data type coroutine returns
    {
         //load the csv
         //the csv needs to be in a folder called Resources
        TextAsset sensorData = Resources.Load<TextAsset>("csv3");

        //split by line and put it in a string array
        string[] data = sensorData.text.Split(new char[] { '\n' });

        //run through the line
        for(int i = 1; i < data.Length; i++ )
        {
            //split by comma and place in a string array
            string[] row = data[i].Split(new char[] {','}); //might not need the new char[] method and use only Split()
            
            //check if name is valid and not empty
            if(row[i] != "") //if the row isn't empty then do the loop
            {
            //initiate the Sensors class
            Sensors s = new Sensors();
            //casting a string to an int with
            //int.TryParse(waht to parse, out where to put it in)
            int.TryParse(row[0], out s.id);
            s.sensorName = row[1];
            s.data = row[2];
            int.TryParse(row[3], out s.time);
            int.TryParse(row[4], out s.date);

            //add them to the list of sensors
            sensors.Add(s);
            }
        }

        foreach(Sensors s in sensors)
        {
            //print the list of sensors to console
            Debug.Log("ID: " + s.id + " Name: " + s.sensorName + " Data: " + s.data + " Time " + s.time + " Date " + s.date);
            //return the data in inervals of seconds
            yield return new WaitForSeconds(1.0f); 
        }
    }
}
