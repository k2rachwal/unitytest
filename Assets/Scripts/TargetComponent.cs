using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : MonoBehaviour, IRestartableObject
{
    private Rigidbody2D m_rigidbody;
    private ParticleSystem m_particles;
    //private bool m_hitTheTarget = false;
    private Vector3 m_startPosition;
    private Quaternion m_startRotation;


    private void DoPlay()
    {
        m_rigidbody.simulated = true;
    }


    private void DoPause()
    {
        m_rigidbody.simulated = false;
    }


    public void DoRestart()
    {
        transform.position = m_startPosition;
        transform.rotation = m_startRotation;


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            //m_hitTheTarget = true;
            m_particles.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_particles = GetComponentInChildren<ParticleSystem>();
        m_startPosition = transform.position;
        m_startRotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            
           // Restart();
        }
        
        //if (m_hitTheTarget)
        //{
        //    Debug.Log("is HIT");

        //}

    }
    void OnDestroy()
    {
        GameplayManager.OnGamePaused -= DoPause;
        GameplayManager.OnGamePlaying -= DoPlay;
    }

  
}
