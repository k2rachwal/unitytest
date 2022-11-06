using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    int frames;

    // Start is called before the first frame update
    void Start()
    {
        frames = 0;
        //Debug.Log("Hello World!");
 
    }
    // Update is called once per frame
    void Update()
    {
        ++frames;
        Debug.Log("Frames passed: " + frames);
        Debug.Log("Time since last frame: " + Time.deltaTime);
        Debug.Log("Current frames per second: " + (1 / Time.deltaTime));
    }
}
