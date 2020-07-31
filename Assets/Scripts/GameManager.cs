using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : FloatEventInvoker {

    public static GameManager Instance { get; set; }
    public static GameStateName CurrentState { get; set; }

    private void Awake()
    {
        ScreenUtils.Initialize();
        NumberConverter.Initialize();
        EventManager.Initialize();
    }

    // Use this for initialization
    void Start () {
        Instance = this;

        //events
        unityEvents.Add(EventName.GameStarted, new GameStarted());
        EventManager.AddInvoker(EventName.GameStarted, this);

        unityEvents.Add(EventName.GameEnded, new GameEnded());
        EventManager.AddInvoker(EventName.GameEnded, this);

        SetGameState(GameStateName.Preperation);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0) && CurrentState == GameStateName.Preperation)
        {
            SetGameState(GameStateName.Gameplay);
        }
    }

    public void SetGameState(GameStateName newGameState)
    {
        CurrentState = newGameState;

        switch(newGameState)
        {
            case GameStateName.Gameplay:
                {
                    unityEvents[EventName.GameStarted].Invoke(0);
                    break;
                }
            case GameStateName.GameOver:
                {
                    SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
                    unityEvents[EventName.GameEnded].Invoke(0);
                    break;
                }
        }
    }
}
