using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDisplayBuild : MonoBehaviour
{
    public GameObject blockPrefab;
    public int height = 9;
    public int width = 9;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 blockScale = blockPrefab.transform.localScale;
        float startX = transform.position.x - (((width - 1) / 2) * blockScale.x);
        float startY = transform.position.y - (((height - 1) / 2) * blockScale.y);

        Vector3 startPosition = new Vector3(startX, startY, transform.position.z);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Vector3 objPosition = new Vector3(startX + (blockScale.x * j), startY + (blockScale.y * i), transform.position.z);

                Instantiate(blockPrefab, objPosition, blockPrefab.transform.rotation, transform);
            }
        }



    }
}
