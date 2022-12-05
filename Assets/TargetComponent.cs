using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    private ParticleSystem m_particles;
    //private bool m_hitTheTarget = false;

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
        m_particles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (m_hitTheTarget)
        //{
        //    Debug.Log("is HIT");
          
        //}

    }
}
