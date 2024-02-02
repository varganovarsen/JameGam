using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class GameStateController : MonoBehaviour
{
    public static GameStateController instance;
    public static GameState currentState;

    public static event Action<GameState> ChangeState;

    // State Parameters
    public static bool CanAct = true;
  
    public static bool timeRunning = true;
    

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SetState(GameState state)
    {
        currentState = state;
        ChangeState.Invoke(state);

        switch (currentState)
        {
            case GameState.meeting:
                CanAct = true;
                timeRunning = true;
            break;

            case GameState.guessing:
                CanAct = false;
                timeRunning = true;
            break;

            case GameState.pause:
                CanAct = false;
                timeRunning = false;
            break;

            case GameState.menu:
                CanAct = false;
                timeRunning = true;
            break;

            case GameState.cutscene:
                CanAct = false;
                timeRunning = true;
            break;

            default:
            Debug.Log("There is no such game state");
            return;
        }

        // Applying game state

        Time.timeScale = timeRunning ? 1 : 0;


    }

}



public enum GameState
{
    meeting,
    guessing,
    pause,
    menu,
    cutscene
}
