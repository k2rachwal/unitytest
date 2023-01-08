using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    float m_overallTime = 0.0f;
    float m_timeSinceLastSave = 0.0f;

    public void SaveSettings()
    {
        m_overallTime += m_timeSinceLastSave;
        Debug.Log("Saving overall time value: " + m_overallTime);
        PlayerPrefs.SetFloat("OverallTime", m_overallTime);
        m_timeSinceLastSave = 0.0f;
    }

    public void LoadSettings()
    {
        m_overallTime = PlayerPrefs.GetFloat("OverallTime", 0.0f);
        Debug.Log("Loaded overall time value: " + m_overallTime);
    }

    public void Start()
    {
        m_timeSinceLastSave = 0.0f;
        LoadSettings();
        Debug.Log(Application.dataPath);
    }
    private void Update()
    {
        m_timeSinceLastSave += Time.deltaTime;
    }
}