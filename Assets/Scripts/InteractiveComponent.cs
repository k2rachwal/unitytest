using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractiveComponent : MonoBehaviour, IRestartableObject
{
    protected Rigidbody2D m_rigidbody;
    protected Vector3 m_startPosition;
    protected Quaternion m_startRotation;

    private void DoPlay()
    {
        m_rigidbody.simulated = true;
        Debug.Log("do play");
    }

    public virtual void DoRestart()
    {
        transform.position = m_startPosition;
        transform.rotation = m_startRotation;
    }
    private void DoPause()
    {
        m_rigidbody.simulated = false;
    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
        m_startPosition = transform.position;
        m_startRotation = transform.rotation;

    }

    void Update()
    {


    }
    void OnDestroy()
    {
        GameplayManager.OnGamePaused -= DoPause;
        GameplayManager.OnGamePlaying -= DoPlay;
    }

   
}