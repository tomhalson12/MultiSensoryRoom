using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTracking : MonoBehaviour
{
    public bool useFoveEyeTracking = false;
    public FoveInterface fove;
    public float sphereRadius = 10f;

    private FoveInterfaceBase.EyeRays eyeRay;
    private Ray ray;

    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if (useFoveEyeTracking)
        {
            eyeRay = fove.GetGazeRays();
            ray = eyeRay.left;
        } else
        {
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        }

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f))
        {
            GameObject centreObject = hit.collider.gameObject;

            if(centreObject.layer == LayerMask.NameToLayer("Block"))
            {
                BlockInteraction centreBlock = centreObject.GetComponent<BlockInteraction>();
                if(centreBlock != null)
                {
                    centreBlock.Move();
                }

                Collider[] surrounding = Physics.OverlapSphere(centreObject.transform.position, sphereRadius);
                

                foreach (Collider collider in surrounding)
                {
                    GameObject gameObj = collider.gameObject;

                    if(centreObject.GetInstanceID() != gameObj.GetInstanceID() && gameObj.layer == LayerMask.NameToLayer("Block"))
                    {
                        BlockInteraction surroundingBlock = gameObj.GetComponent<BlockInteraction>();
                        if (surroundingBlock != null)
                        {
                            surroundingBlock.HalfMove();
                        }
                    }
                }
            }
        }
    }
}
