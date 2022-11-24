using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private SpringJoint2D m_connectedJoint;
    private Rigidbody2D m_connectedBody;
    public float SlingStart = 0.5f;
    public float MaxSpringDistance = 13f;
    private LineRenderer m_lineRenderer;
    private TrailRenderer m_trailRenderer;
    private bool m_hitTheGround = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_hitTheGround = true;
        }
    }
    private void OnMouseDrag()
    {
        m_trailRenderer.enabled = false;
        Vector2 halfSpling = new Vector2(m_connectedBody.position.x + 0.5f, m_connectedBody.position.y);
        Vector2 halfSpling2 = new Vector2(m_connectedBody.position.x - 0.5f, m_connectedBody.position.y);
        m_lineRenderer.positionCount = 3;
        m_lineRenderer.SetPositions(new Vector3[] { halfSpling2, transform.position, halfSpling });
        m_rigidbody.simulated = false;
        if (GameplayManager.Instance.Pause)
        {
            return;
        }
    
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 newBallPos = new Vector3(worldPos.x, worldPos.y);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        float CurJointDistance = Vector3.Distance(newBallPos, m_connectedBody.transform.position);
        if (CurJointDistance > MaxSpringDistance)
        {
            Vector2 direction = (newBallPos - m_connectedBody.position).normalized;
            transform.position = m_connectedBody.position + direction * MaxSpringDistance;
        }
        else
        {
            transform.position = newBallPos;
        }
        m_hitTheGround = false;
    }
    private void OnMouseUp()
        {
        m_rigidbody.simulated = true;
        }

    public bool IsSimulated()
        {
        return m_rigidbody.simulated;
        }

    public float GetPhysicsSpeed()
        {
        return m_rigidbody.velocity.magnitude;

        }


    private void Start()
        {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_connectedJoint = GetComponent<SpringJoint2D>();
        m_connectedBody = m_connectedJoint.connectedBody;
        m_trailRenderer = GetComponent<TrailRenderer>();
        m_trailRenderer.enabled = false;


    }


    private void Update()
    {
        
        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart)
        {
            m_connectedJoint.enabled = false;
            m_lineRenderer.enabled = false;
            m_trailRenderer.enabled = true;
        }
        m_trailRenderer.enabled = !m_hitTheGround;
    }
}
