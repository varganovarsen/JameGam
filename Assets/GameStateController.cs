using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public static GameStateController instance;
    public static GameState currentState;

   
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
        

    }

}

public enum GameState
{
    meeting,
    guessing,
    pause,
    menu
}
