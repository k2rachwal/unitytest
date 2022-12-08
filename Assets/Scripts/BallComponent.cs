using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour, IRestartableObject

{
    private Rigidbody2D m_rigidbody;
    private SpringJoint2D m_connectedJoint;
    private Rigidbody2D m_connectedBody;
    public float SlingStart = 0.5f;
    public float MaxSpringDistance = 0.01f;
    private LineRenderer m_lineRenderer;
    private TrailRenderer m_trailRenderer;
    private bool m_hitTheGround = false;
    private Vector3 m_startPosition;
    private Quaternion m_startRotation;
    private AudioSource m_audioSource;
    public AudioClip PullSound;
    public AudioClip ShootSound;
    public AudioClip HitTheGroundSound;
    public AudioClip RestartSound;
    private Animator m_animator;
    private ParticleSystem m_particles;

    private void DoPlay()
    {
        m_rigidbody.simulated = true;
    }


    private void DoPause()
    {
        m_rigidbody.simulated = false;
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_animator.enabled = true;
        m_animator.Play(0); // 0 tutaj to nr warstwy animacji, gdzie domyślna to 0
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_hitTheGround = true;
        }
    }

    private void SetLineRendererPoints()
    {
        Vector3 halfSpling = new Vector3(m_connectedBody.position.x + 0.5f, m_connectedBody.position.y, m_connectedBody.transform.position.z);
        Vector2 halfSpling2 = new Vector2(m_connectedBody.position.x - 0.5f, m_connectedBody.position.y);
        m_lineRenderer.positionCount = 3;
        m_lineRenderer.SetPositions(new Vector3[] { halfSpling2, transform.position, halfSpling });
       
    }

    private void OnMouseDrag()
    {
        m_audioSource.PlayOneShot(PullSound);
        m_trailRenderer.enabled = false;
        m_rigidbody.simulated = false;

        SetLineRendererPoints();

        //if (GameplayManager.Instance.Pause)
        //{
         //   return;
        //}
    
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 newBallPos = new Vector3(worldPos.x, worldPos.y);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        float CurJointDistance = Vector3.Distance(newBallPos, m_connectedBody.transform.position);
        if (CurJointDistance > MaxSpringDistance)
        {
            Vector2 direction = (newBallPos - m_connectedBody.position).normalized;
            transform.position = m_connectedBody.position + (direction * MaxSpringDistance);
        }
        else
        {
            transform.position = newBallPos;
        }

        m_hitTheGround = false;

        
        
    }

    private void OnMouseUp()
    {
        m_audioSource.PlayOneShot(ShootSound);
        m_rigidbody.simulated = true;
        m_particles.Play();
    }

    public bool IsSimulated()
    {
        return m_rigidbody.simulated;
    }

    public float GetPhysicsSpeed()
    {
        return m_rigidbody.velocity.magnitude;
    }

    public void DoRestart()
    {
        transform.position = m_startPosition;
        transform.rotation = m_startRotation;

        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = 0.0f;
        m_rigidbody.simulated = true;

        m_connectedJoint.enabled = true;
        m_lineRenderer.enabled = true;
        m_trailRenderer.enabled = false;

        SetLineRendererPoints();
        

    }



    private void Start()
    {
        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
        m_lineRenderer = GetComponent<LineRenderer>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_connectedJoint = GetComponent<SpringJoint2D>();
        m_connectedBody = m_connectedJoint.connectedBody;
        m_trailRenderer = GetComponent<TrailRenderer>();
        m_trailRenderer.enabled = false;
        m_startPosition = transform.position;
        m_startRotation = transform.rotation;
        m_audioSource = GetComponent<AudioSource>();
        m_animator = GetComponentInChildren<Animator>();
        m_particles = GetComponentInChildren<ParticleSystem>();

    }


    private void Update()
    {
        
        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart)
        {
            m_connectedJoint.enabled = false;
            m_lineRenderer.enabled = false;
            m_trailRenderer.enabled = true;
            //Debug.Log("działa");
        }
        if (m_hitTheGround)
        {
            m_trailRenderer.enabled = false;
            m_audioSource.PlayOneShot(HitTheGroundSound);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
           // m_audioSource.PlayOneShot(RestartSound);
           // Restart();
        }
    }
    void OnDestroy()
    {
        GameplayManager.OnGamePaused -= DoPause;
        GameplayManager.OnGamePlaying -= DoPlay;
    }

    
}
