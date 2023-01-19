/**using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button PauseButton2;
    public Button PlayButton;
    public Button OptionsButton;
    public Button QuitButton;

    public GameObject MainPanel;
    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;

    void Start()
    {
        PauseButton2.onClick.AddListener(delegate { ShowMainMenu(); });
        PlayButton.onClick.AddListener(delegate { OnPlay(); });
        OptionsButton.onClick.AddListener(delegate { ShowOptions(true); });
        QuitButton.onClick.AddListener(delegate { OnQuit(); });

        SetPanelVisible(false);
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void SetPanelVisible(bool visible)
    {
        MainPanel.SetActive(visible);
    }

    private void OnPlay()
    {
        SetPanelVisible(false);
    }

    public void ShowMainMenu()
    {
        MainPanel.SetActive(true);
    }
    public void ShowOptions(bool bShow)
    {
        OptionsPanel.SetActive(bShow);
        MainMenuPanel.SetActive(!bShow);
    }

    private void OnQuit()
    {
        Application.Quit();
    }
}**/