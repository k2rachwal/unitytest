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
    void Update()
        {

        }


    void FixedUpdate()
        {

        BallSpeed = followTarget.GetPhysicsSpeed();

        if (!followTarget.IsSimulated())
                return;
            transform.position = Vector3.MoveTowards(transform.position, originalPosition + followTarget.transform.position, BallSpeed);

        }
    
}