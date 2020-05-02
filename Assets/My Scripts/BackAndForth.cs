using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public GameObject relativeObject;
    public Vector3 direction = new Vector3(0,0,0);
    public Vector3 offset = new Vector3(0, 0, 0);
    public float speed;

    private bool towardsStart = true;
    private double waypointRadius = 0.1;
    private Vector3 start;
    private Vector3 end;

    // Start is called before the first frame update
    void Start()
    {
        towardsStart = Random.Range(0,2) == 0;

        float startX = direction.x == 0 ? transform.position.x : relativeObject.transform.position.x;
        float startY = direction.y == 0 ? transform.position.y : relativeObject.transform.position.y;
        float startZ = direction.z == 0 ? transform.position.z : relativeObject.transform.position.y;

        start = new Vector3(startX, startY, startZ) + offset;

        end = start + direction;

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(towardsStart ? start : end, transform.position) < waypointRadius)
        {
            towardsStart = !towardsStart;
        }

        transform.position = Vector3.MoveTowards(transform.position, towardsStart ? start : end, Time.deltaTime * speed);
    }
}
