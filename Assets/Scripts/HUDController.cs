using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Button PauseButton;
    public TMPro.TextMeshProUGUI PointsText;

    public void UpdatePoints(int points)
    {
        PointsText.text = "Points: " + points;
    }
    // Start is called before the first frame update
    void Start()
    {
        PauseButton.onClick.AddListener(delegate {
            GameplayManager.Instance.PlayPause();
        });
    }

  
}
