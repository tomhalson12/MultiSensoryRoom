using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    public KeyCode increaseKey;
    public KeyCode decreseKey;

    private float volume;

    // Start is called before the first frame update
    void Start()
    {
        volume = AudioListener.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(increaseKey))
        {
            IncreaseVolume();
        }

        if (Input.GetKeyUp(decreseKey))
        {
            DecreaseVolume();
        }
    }

    void IncreaseVolume()
    {
        volume += 0.2f;

        if(volume > 1)
        {
            volume = 1;
        }

        AudioListener.volume = volume;
    }

    void DecreaseVolume()
    {
        volume -= 0.2f;

        if (volume < 0)
        {
            volume = 0;
        }

        AudioListener.volume = volume;
    }
}
