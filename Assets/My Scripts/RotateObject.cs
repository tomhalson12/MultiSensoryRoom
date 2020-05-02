using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis {
    X,
    Y,
    Z
}

[System.Serializable]
public class Timer
{
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


public class RotateObject : MonoBehaviour, StatTrackerInterface
{
    public KeyCode keyToPress;
    public Axis axis;
    public float rotationValue;

    private Quaternion newRotation;
    private Quaternion originalRotation;
    private bool rotating = false;

    private bool awayFromStart = true;

    private Timer upTimer;
    private Timer downTimer;
    private bool up = true;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
        Vector3 eulerAngles = originalRotation.eulerAngles;
        newRotation = new Quaternion();

        if(axis == Axis.X)
        {
            newRotation.eulerAngles = new Vector3(rotationValue, eulerAngles.y, eulerAngles.z);
        } else if (axis == Axis.Y)
        {
            newRotation.eulerAngles = new Vector3(eulerAngles.x, rotationValue, eulerAngles.z);
        } else
        {
            newRotation.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, rotationValue);
        }

        upTimer = new Timer();
        downTimer = new Timer();

        upTimer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(keyToPress))
        {
            BeginRotate();
        }

        if(transform.rotation == (awayFromStart ? newRotation : originalRotation))
        {
            rotating = false;
            awayFromStart = !awayFromStart;
        }

        if (rotating)
        {
            var step = 20 * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, awayFromStart ? newRotation : originalRotation, step);
        }
    }

    public void BeginRotate()
    {
        rotating = true;
        if (up)
        {
            upTimer.End();
            downTimer.Start();
        } else
        {
            downTimer.End();
            upTimer.Start();
        }
        up = !up;
    }

    public Dictionary<string, float> GetStats()
    {
        Dictionary<string, float> dic = new Dictionary<string, float>();
        if (up)
        {
            upTimer.UpdateTime();
        } else
        {
            downTimer.UpdateTime();
        }
        
        dic.Add("Up", upTimer.accumulatedTime);
        dic.Add("Down", downTimer.accumulatedTime);

        return dic;
    }
}
