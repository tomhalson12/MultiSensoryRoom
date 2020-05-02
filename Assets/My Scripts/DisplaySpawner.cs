using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DisplayType
{
    public GameObject displayPrefab;
    public string name;

    private float startTime;
    private float lastUpdated;
    public float accumulatedTime;

    public void Start()
    {
        startTime = Time.time;
    }

    public void End()
    {
        lastUpdated = Time.time;
        accumulatedTime += (lastUpdated - startTime);
        startTime = 0;
    }

    public void UpdateTime()
    {
        lastUpdated = Time.time;
        accumulatedTime += (lastUpdated - startTime);
        startTime = lastUpdated;
    }
}

public class DisplaySpawner : MonoBehaviour, StatTrackerInterface
{
    public DisplayType[] displayObjects;
    public KeyCode keyToChange;
    public float topOfBase = 0.45f;

    private GameObject currentObj;
    private Vector3 objPosition;
    private Quaternion objRotation;

    private int current = 0;

    // Start is called before the first frame update
    void Start()
    {
        objPosition = transform.position + new Vector3(0, topOfBase, 0);
        objRotation = transform.rotation;

        currentObj = Instantiate(displayObjects[current].displayPrefab, objPosition, objRotation, transform);

        displayObjects[current].Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(keyToChange))
        {
            DisplayNext();
        }
    }

    void DisplayNext()
    {
        if(currentObj != null)
        {
            displayObjects[current].End();
            Destroy(currentObj);
        }

        if (++current >= displayObjects.Length)
        {
            current = 0;
        }

        currentObj = Instantiate(displayObjects[current].displayPrefab, objPosition, objRotation, transform);

        displayObjects[current].Start();
    }

    public Dictionary<string, float> GetStats()
    {
        Dictionary<string, float> dic = new Dictionary<string, float>();
        displayObjects[current].UpdateTime();

        foreach (DisplayType display in displayObjects)
        {
            dic.Add(display.name, display.accumulatedTime);
        }

        return dic;
    }
}
