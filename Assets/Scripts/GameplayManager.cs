 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System; // musimy to dodać, by móc obsługiwać wyjątki

public class GameplayManager : Singleton<GameplayManager>
{
    List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();
    private HUDController m_HUD;
    private int m_points = 0;

    private void TestThrow()
    {
        throw new NullReferenceException("Test exception");
    }

    public int Points
    {
        get { return m_points; }
        set
        {
            m_points = value;
            m_HUD.UpdatePoints(m_points);
        }
    }
    private void GetAllRestartableObjects()
    {
        m_restartableObjects.Clear();

        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            IRestartableObject[] childrenInterfaces = rootGameObject.GetComponentsInChildren<IRestartableObject>();

            foreach (var childInterface in childrenInterfaces)
                m_restartableObjects.Add(childInterface);
        }
    }


    public enum EGameState
    {
        Playing,
        Paused
    }

    private EGameState m_state;

    public EGameState GameState
    {
        get { return m_state; }
        set { m_state = value;
            switch (m_state)
            {
                case EGameState.Paused:
                    {
                        if (OnGamePaused != null)
                            OnGamePaused();
                    }
                    break;

                case EGameState.Playing:
                    {
                        if (OnGamePlaying != null)
                            OnGamePlaying();
                    }
                    break;
            }
        }
    }
    public static event GameStateCallback OnGamePaused;
    public static event GameStateCallback OnGamePlaying;
    public delegate void GameStateCallback();
    public bool Pause = false;
    // Start is called before the first frame update
   
        private void Start()
        {
            m_state = EGameState.Playing;
            GetAllRestartableObjects();
            m_HUD = FindObjectOfType<HUDController>();
            Points = 0;

        int[] Test = new int[2] { 0, 0 };
        //Test[2] = 1;
        try
        {
            Test[2] = 1;
            TestThrow();
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.Log("Index Exception: " + e.Message);
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
    }
        //Pause = false;

    

    // Update is called once per frame

    public void Restart()
    {
        Points = 0;
        foreach (var restartableObject in m_restartableObjects)
            restartableObject.DoRestart();
    }

    public void PlayPause()
    {
        switch (GameState)
        {
            case EGameState.Playing: { GameState = EGameState.Paused; } break;
            case EGameState.Paused: { GameState = EGameState.Playing; } break;
        }
    }

    void Update()

    {
        if (Input.GetKeyUp(KeyCode.Escape))
        GameState = EGameState.Paused;
        if (Input.GetKeyUp(KeyCode.R))
            Restart();
        if (Input.GetKeyUp(KeyCode.Space))
            PlayPause();
        /*if (Input.GetKeyUp(KeyCode.Space))
        {
            switch (GameState)
            {
                case EGameState.Playing:
                    {
                        GameState = EGameState.Paused;
                        Debug.Log("paused");
                    }
                    break;

                case EGameState.Paused:
                    {
                        GameState = EGameState.Playing;
                        Debug.Log("play");
                    }
                    break;
            }
        }*/

        //if (Input.GetKeyUp(KeyCode.Space))
        //    Pause = !Pause;

        // if (!Input.GetKeyUp(KeyCode.Escape))
        // {
        //     Application.Quit();
        // }


    }
}
