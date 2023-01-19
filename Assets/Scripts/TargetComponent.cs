using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : InteractiveComponent
{
    
    private ParticleSystem m_particles;
  
    public override void DoRestart()
    {
        base.DoRestart();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {  
            m_particles.Play();
            GameplayManager.Instance.Points += 1;
            GameObject.Destroy(this.gameObject, 1.0f);
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_particles = GetComponentInChildren<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {


    }

  
}
