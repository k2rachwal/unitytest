using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameplayManager;

public class PauseMenuController : MonoBehaviour
{
    List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();
    private int m_points = 0;
    public int Points;
    public Button ResumeButton;
    public Button QuitButton;
    public Button Restart;
    public Button Options;
    public GameObject Panel;
    public GameObject Areyousure;
    public GameObject OptionsPanel;
    public Button Yes;
    public Button No;
    public Button AcceptButton;
    //public Button CancelButton;
    public Button Back;
    private float m_initialVolume = 0.0f;

    public void SetPanelVisible(bool visible)
    {
        Panel.SetActive(visible);
    }
    public void SetAreyousureVisible(bool visible)
    {
        Areyousure.SetActive(visible);
    }
    public void SetOptionsVisible(bool visible)
    {
        OptionsPanel.SetActive(visible);
    }

    private void OnPause()
    {
        SetPanelVisible(true);
        SetAreyousureVisible(false);
    }
    private void OnGamePlaying()
    {
        SetPanelVisible(false);
        SetAreyousureVisible(false);
    }

    private void OnResume()
    {
        GameplayManager.Instance.GameState = EGameState.Playing;
        SetPanelVisible(false);
        SetAreyousureVisible(false);
    }

    private void OnQuit()
    {
        SetPanelVisible(false);
        SetAreyousureVisible(true);
    }
    private void OnYes()
    {
        SaveManager.Instance.SaveSettings();
        Application.Quit();
    
    }
    private void OnNo()
    {
        SetPanelVisible(true);
        SetAreyousureVisible(false);
    }
    private void OnOptions()
    {
        SetOptionsVisible(true);
        SetPanelVisible(false);
    }
    private void OnBack()
    {
        SetOptionsVisible(false);
        SetPanelVisible(true);
    }


    private void OnRestart()
    {
        GameplayManager.Instance.Restart();
        Panel.SetActive(false);
        Points = 0;
            foreach (var restartableObject in m_restartableObjects)
                restartableObject.DoRestart();
        
    }
    private void OnEnable()
    {
        m_initialVolume = AudioListener.volume;
    }

    private void OnAccept()
    {
        SaveManager.Instance.SaveData.m_masterVolume = AudioListener.volume;
        SaveManager.Instance.SaveSettings();
        SetOptionsVisible(false);
        SetPanelVisible(true);
    }

    private void OnCancel()
    {
        AudioListener.volume = m_initialVolume;
        SetOptionsVisible(false);
        SetPanelVisible(true);
    }



    // Start is called before the first frame update
    void Start()
    {
        ResumeButton.onClick.AddListener(delegate { OnResume(); });
        QuitButton.onClick.AddListener(delegate { OnQuit(); });
        Restart.onClick.AddListener(delegate { OnRestart(); });
        Yes.onClick.AddListener(delegate { OnYes(); });
        No.onClick.AddListener(delegate { OnNo(); });
        Options.onClick.AddListener(delegate { OnOptions(); });
        AcceptButton.onClick.AddListener(delegate { OnAccept(); });
        Back.onClick.AddListener(delegate { OnCancel(); });


        SetPanelVisible(false);
        SetAreyousureVisible(false);
        SetOptionsVisible(false);

        GameplayManager.OnGamePaused += OnPause;
        GameplayManager.OnGamePlaying += OnGamePlaying;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
