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
    public GameObject Panel;
    public GameObject Areyousure;
    public Button Yes;
    public Button No;

    public void SetPanelVisible(bool visible)
    {
        Panel.SetActive(visible);
    }
    public void SetAreyousureVisible(bool visible)
    {
        Areyousure.SetActive(visible);
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
        Application.Quit();
    
    }
    private void OnNo()
    {
        SetPanelVisible(true);
        SetAreyousureVisible(false);
    }
    private void OnRestart()
    {
        GameplayManager.Instance.Restart();
        Panel.SetActive(false);
        Points = 0;
            foreach (var restartableObject in m_restartableObjects)
                restartableObject.DoRestart();
        
    }


   
    // Start is called before the first frame update
    void Start()
    {
        ResumeButton.onClick.AddListener(delegate { OnResume(); });
        QuitButton.onClick.AddListener(delegate { OnQuit(); });
        Restart.onClick.AddListener(delegate { OnRestart(); });
        Yes.onClick.AddListener(delegate { OnYes(); });
        No.onClick.AddListener(delegate { OnNo(); });

        SetPanelVisible(false);
        SetAreyousureVisible(false);

        GameplayManager.OnGamePaused += OnPause;
        GameplayManager.OnGamePlaying += OnGamePlaying;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
