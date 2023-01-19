using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[System.Serializable]
public struct GameSaveData
{
    public float m_timeSinceLastSave;
    public float m_overallTime;
    public int m_hitsfromlastgame;
    public float m_masterVolume;

}


public class SaveManager : Singleton<SaveManager>
{
    //float m_overallTime = 0.0f;
    //float m_timeSinceLastSave = 0.0f;
    //int m_hitsfromlastgame = GameplayManager.Instance.LifetimeHits;
    int m_Allhits;
    public GameSaveData SaveData;
    private string m_pathBin;
    private string m_pathJSON;
    [Header("hello")]
    public bool UseBinary = true;

    private void ApplySettings()
    {
        AudioListener.volume = SaveData.m_masterVolume;
    }
    public void SaveSettings()
    {
        SaveData.m_overallTime += SaveData.m_timeSinceLastSave;
        //Debug.Log("Saving overall time value: " + m_overallTime);
        SaveData.m_timeSinceLastSave = 0.0f;
        // PlayerPrefs.SetFloat("OverallTime", m_overallTime);


        //PlayerPrefs.SetInt("Hits from last game", m_hitsfromlastgame);
        SaveData.m_hitsfromlastgame = GameplayManager.Instance.LifetimeHits;

        if (UseBinary)
        {
            FileStream file = new FileStream(m_pathBin, FileMode.OpenOrCreate);
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(file, SaveData);
            file.Close();
        }
        else
        {
            string saveData = JsonUtility.ToJson(SaveData);
            File.WriteAllText(m_pathJSON, saveData);
        }


    }

    public void LoadSettings()
    {

        SaveData.m_overallTime = 0.0f;
        //m_overallTime = PlayerPrefs.GetFloat("OverallTime", 0.0f);
        //Debug.Log("Loaded overall time value: " + m_overallTime);

        //m_Allhits = PlayerPrefs.GetInt("All hits", 0);
        //Debug.Log("All hits value: " + m_Allhits);


        if (UseBinary && File.Exists(m_pathBin))
        {
            FileStream file = new FileStream(m_pathBin, FileMode.Open);
            BinaryFormatter binFormat = new BinaryFormatter();
            SaveData = (GameSaveData)binFormat.Deserialize(file);
            file.Close();
        }
        else if (!UseBinary && File.Exists(m_pathJSON))
        {
            string saveData = File.ReadAllText(m_pathJSON);
            SaveData = JsonUtility.FromJson<GameSaveData>(saveData);
        }
        else
        {
            SaveData.m_timeSinceLastSave = 0.0f;
            ApplySettings();


        }
        
    }

    public void Start()
    {
        SaveData.m_masterVolume = AudioListener.volume;
        LoadSettings();
        GameplayManager.Instance.LifetimeHits = SaveData.m_hitsfromlastgame;
        SaveData.m_timeSinceLastSave = 0.0f;
        Debug.Log(Application.dataPath);
        //Debug.Log(m_hits);
        m_pathBin = Path.Combine(Application.persistentDataPath, "save.bin");
        m_pathJSON = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log("Ścieżka:" + Application.persistentDataPath);
        
    }
    private void Update()
    {
        SaveData.m_timeSinceLastSave += Time.deltaTime;
        
    }
}

