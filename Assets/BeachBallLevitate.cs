using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBallLevitate : MonoBehaviour
{
    /*wartość pozycji startowej*/
    private Vector3 m_startPosition;

    /*tworzymy globalne zmienne cyklicznie zmieniających się wartości*/
    private float m_curYPos = 0.0f;
    private float m_curZRot = 0.0f;
    private float m_curScale = 1.0f;
    /*wystawiamy parametry publiczne do wygodnej modyfikacji*/
    public float Amplitude = 1.0f;
    public float RotationSpeed = 50;
    public float ScaleAplitude = 0.03f;

    void Start()
    {
        /*zapamiętujemy pozycję startową*/
        m_startPosition = transform.position;
    }

    void Update()
    {
        /*zmiana pozycji*/
        m_curYPos = Mathf.PingPong(Time.time, Amplitude) - Amplitude * 0.5f;
        transform.position = new Vector3(m_startPosition.x,
                                         m_startPosition.y + m_curYPos,
                                         m_startPosition.z);
        /*zmiana rotacji*/
        m_curZRot += Time.deltaTime * RotationSpeed;
        transform.rotation = Quaternion.Euler(0, 0, m_curZRot);

        //skala
        m_curScale = Mathf.PingPong(Time.time, ScaleAplitude) - ScaleAplitude * 0.5f;
        transform.localScale = new Vector3(transform.localScale.x + m_curScale, transform.localScale.y + m_curScale, transform.localScale.z);

    }
}
