using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightColor
{
    public Color color;
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


public class ChangeLightColour : MonoBehaviour, StatTrackerInterface
{
    public Light[] lights;
    public KeyCode keyToPress;
    public float lightIntensity;
    public List<LightColor> colors;

    private int current = 0;
    private bool lightsOn = true;

    private float keyPressedTime = 0;

    void Start()
    {
        foreach (Light lt in lights)
        {
            lt.color = colors[0].color;
        }
        colors[0].Start();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            keyPressedTime = Time.time;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            float timeKeyHeld = Time.time - keyPressedTime;

            if (timeKeyHeld >= .5)
            {
                OnOff();
            } else
            {
                ChangeColor();
            }
        }
    }

    public void ChangeColor()
    {
        if (lightsOn)
        {
            colors[current].End();

            if (++current >= colors.Count)
            {
                current = 0;
            }

            foreach (Light lt in lights)
            {
                lt.color = colors[current].color;
            }

            colors[current].Start();
        }
    }

    public void OnOff()
    {
        lightsOn = !lightsOn;
        foreach (Light lt in lights)
        {
            if (lightsOn)
            {
                lt.intensity = lightIntensity;
            } else
            {
                lt.intensity = 0;
            }
        }

        if (lightsOn)
        {
            colors[current].Start();
        } else
        {
            colors[current].End();
        }
    }

    public Dictionary<string, float> GetStats()
    {
        Dictionary<string, float> dic = new Dictionary<string, float>();
        colors[current].UpdateTime();

        foreach (LightColor color in colors)
        {
            dic.Add(color.name, color.accumulatedTime);
        }

        return dic;
    }
}
