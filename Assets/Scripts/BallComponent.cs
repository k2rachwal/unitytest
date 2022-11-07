using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    public float Speed = 1.0f;


    void Start()
    {

    }



    private void Update()
    {
        if (transform.localScale.x < 3.0f)
        {
            transform.localScale += Speed * Time.deltaTime * Vector3.up;
            transform.localScale += Speed * Time.deltaTime * Vector3.right;
            transform.localScale += Speed * Time.deltaTime * Vector3.forward;
        }


    }
}
