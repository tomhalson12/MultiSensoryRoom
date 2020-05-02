using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class TrackedObject
{
    public string name;
    public GameObject trackedObject;
}

public class StatTracker : MonoBehaviour
{
    public TrackedObject[] trackedObjects;
    public string fileName;

    private Dictionary<string, float>[] stats;

    private Dictionary<string, Dictionary<string, float>> objectStats = new Dictionary<string, Dictionary<string, float>>();

    private void OnApplicationQuit()
    {
        GetStats();
        StatsToFile();
    }

    void GetStats()
    {
        objectStats.Clear();

        foreach (TrackedObject obj in trackedObjects)
        {
            Dictionary<string, float> dic = obj.trackedObject.GetComponent<StatTrackerInterface>().GetStats();

            objectStats.Add(obj.name, dic);
        }
    }

    void StatsToFile()
    {
        string path = Application.dataPath + "/" + fileName + "_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
    
        File.WriteAllText(path, "Stats: \n");
       

        foreach (KeyValuePair<string, Dictionary<string, float>> obj in objectStats)
        {

            File.AppendAllText(path, "\n" + obj.Key + ":\n");
           

            foreach (KeyValuePair<string, float> stat in obj.Value)
            {
                double percentageOfTime = Math.Round((stat.Value / (Time.time)) * 100, 1);

                File.AppendAllText(path, "  " + stat.Key + " = " + percentageOfTime + "\n");
            }
        }
    }
}
