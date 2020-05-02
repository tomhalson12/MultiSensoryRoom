using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksDisplayMovement : MonoBehaviour
{
    public float outDistance = .5f;
    public float forwardSpeed = 2f;
    public float backSpeed = 0.5f;

    private Vector3 startPoint;
    private Vector3 midPoint;
    private Vector3 endPoint;

    private Vector3 currentDestination;
    private float currentSpeed;
    private bool movingOut = false;

    void Start()
    {
        startPoint = transform.position;
        midPoint = transform.position + transform.forward * (outDistance / 2f);
        endPoint = transform.position + transform.forward * outDistance;

        currentDestination = startPoint;
        currentSpeed = backSpeed;
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

       // transform.position = Vector3.MoveTowards(transform.position, towardsStart ? start : end, Time.deltaTime * speed);
    }

    public void Move()
    {
        movingOut = true;
        currentDestination = endPoint;
        currentSpeed = forwardSpeed;
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.blue, .5f);
    }

    public void HalfMove()
    {
        movingOut = true;
        currentDestination = midPoint;
        currentSpeed = forwardSpeed;
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.blue, Color.red, Time.deltaTime * forwardSpeed); ;
    }
}
