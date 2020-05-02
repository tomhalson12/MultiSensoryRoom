using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public float outDistance = .5f;
    public float forwardSpeed = 2f;
    public float backSpeed = 0.5f;
    public Color[] colors;
    public float colorChangeSeconds = 3f;

    private Vector3 startPoint;
    private Vector3 midPoint;
    private Vector3 endPoint;

    private Vector3 currentDestination;
    private float currentSpeed;
    private bool movingOut = false;

    private int currentColor = 0;

    private float colorTime = 0;

    private Material material;

    private static string EMISSION_COLOR_KEYWORD = "_EmissionColor";
    private static string EMISSION_KEYWORD = "_EMISSION";
    private float intensity = .75f;

    void Start()
    {
        startPoint = transform.position;
        midPoint = transform.position + transform.forward * (outDistance / 2f);
        endPoint = transform.position + transform.forward * outDistance;

        currentDestination = startPoint;
        currentSpeed = backSpeed;

        material = gameObject.GetComponent<Renderer>().material;

        currentColor = colors.Length - 1;

        material.color = colors[currentColor];
    }

    void Update()
    {
        if (transform.position == currentDestination && movingOut)
        {
            movingOut = false;
            currentDestination = startPoint;
            currentSpeed = backSpeed;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentDestination, Time.deltaTime * currentSpeed);

        if (!movingOut)
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        if(material.color != colors[colors.Length - 1])
        {
            if(material.color == colors[currentColor])
            {
                currentColor++;
                colorTime = 0;
            }

            colorTime += Time.deltaTime / colorChangeSeconds;
            material.color = Color.Lerp(colors[currentColor - 1], colors[currentColor], colorTime);
            material.SetColor(EMISSION_COLOR_KEYWORD, Color.Lerp(colors[currentColor - 1], colors[currentColor], colorTime) * intensity);
        } else
        {
            material.DisableKeyword(EMISSION_KEYWORD);
        }
    }

    public void Move()
    {
        movingOut = true;
        currentDestination = endPoint;
        currentSpeed = forwardSpeed;
        currentColor = 0;
        material.color = colors[currentColor];
        material.EnableKeyword(EMISSION_KEYWORD);
        material.SetColor(EMISSION_COLOR_KEYWORD, colors[currentColor] * intensity);

        colorTime = 0;
    }

    public void HalfMove()
    {
        movingOut = true;
        currentDestination = midPoint;
        currentSpeed = forwardSpeed;
        currentColor = 0;
        material.color = colors[currentColor];
        material.EnableKeyword(EMISSION_KEYWORD);
        material.SetColor(EMISSION_COLOR_KEYWORD, colors[currentColor] * intensity);

        colorTime = 0;
    }
}
