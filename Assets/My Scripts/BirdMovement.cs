using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public GameObject origin;
    public float speed = 1;
    public float width = 1;
    public float height = 1;
    public float heightFrequency = 1;
    public bool reverse;

    private float timeCounter = 0;
    private Vector3 centrePoint;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.localPosition;
        centrePoint = origin.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float ang = reverse ? -timeCounter : timeCounter;
        float x = Mathf.Cos(ang) * width;
        float y = Mathf.Cos(ang * heightFrequency) * height;
        float z = Mathf.Sin(ang) * width;

        transform.eulerAngles = Quaternion.LookRotation(centrePoint - transform.position, Vector3.up).eulerAngles + new Vector3(35, reverse ? 270 : 90, 20);
        transform.localPosition = new Vector3(x, y, z) + offset;
    }
}
