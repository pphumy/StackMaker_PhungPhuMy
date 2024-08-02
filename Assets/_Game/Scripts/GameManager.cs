using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{


    public enum GameState
    {
        MainMenu,
        Gameplay,
        Replay,
        Pause,
        Win
    }

    private void Start()
    {
        // Set initial state
        ChangeState(GameState.MainMenu);
    }

    public GameState currentState;
    public void ChangeState(GameState newState)
    {
        currentState = newState;

        // Perform actions based on the state
        switch (currentState)
        {
            case GameState.MainMenu:
                UIManager.Instance.ShowMainUI();
                InputManager.Instance.direction = EDirection.None;
                break;
            case GameState.Gameplay:
                LevelManager.Instance.LoadLevel();
                UIManager.Instance.ShowInGameUI();
                break;
            case GameState.Win:
                UIManager.Instance.StartCoroutine(UIManager.Instance.ShowWinUI());
                
                break;
            case GameState.Replay:
            //UIManager.Instance.StartCoroutine(UIManager.Instance.ShowWinUI());
            case GameState.Pause:
                InputManager.Instance.direction = EDirection.None;
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        ChangeState(GameState.Gameplay);
    }


    public void WinGame()
    {
        ChangeState(GameState.Win);
    }
}
