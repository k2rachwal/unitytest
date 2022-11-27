using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private BallComponent followTarget;
    private Vector3 originalPosition;
    private float BallSpeed;

    void Start()
    {
        // m_rigidbody = GetComponent<Rigidbody2D>();
        followTarget = FindObjectOfType<BallComponent>();
        originalPosition = transform.position;
      
    }
    private void Restart()
    {
        transform.position = new Vector3(0, 0, 0);

    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
            {
            Restart();
            }
    }


    void FixedUpdate()
    {

        BallSpeed = followTarget.GetPhysicsSpeed();

        if (!followTarget.IsSimulated())
                return;
            transform.position = Vector3.MoveTowards(transform.position, originalPosition + followTarget.transform.position, BallSpeed);

    }

    
}