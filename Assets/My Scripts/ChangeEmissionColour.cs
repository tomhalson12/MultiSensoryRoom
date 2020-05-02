using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmissionColour : MonoBehaviour
{
    public Renderer[] renderers;
    public KeyCode keyToPress;
    public List<Color> colors;

    private int current = 0;
    private bool lightsOn = true;

    private float keyPressedTime = 0;

    private float intensity = 3f;
    private static string EMISSION_KEYWORD = "_EmissionColor";

    void Start()
    {
        foreach(Renderer rend in renderers)
        {
            rend.material.SetColor(EMISSION_KEYWORD, colors[0] * intensity);
        }
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

            if(timeKeyHeld >= .5)
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
            if (++current >= colors.Count)
            {
                current = 0;
            }

            foreach (Renderer rend in renderers)
            {
                rend.material.SetColor(EMISSION_KEYWORD, colors[current] * intensity);
            }
        }
    }

    public void OnOff()
    {
        lightsOn = !lightsOn;
        foreach (Renderer rend in renderers)
        {
            if (lightsOn)
            {
                rend.material.SetColor(EMISSION_KEYWORD, colors[current] * intensity);
            } else
            {
                rend.material.SetColor(EMISSION_KEYWORD, Color.black);
            }
        }
        
    }
}
