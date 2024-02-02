using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class AlienGroupController : MonoBehaviour
{

    public List<AlienData> aliens;
    private List<Vector2> _alienPositions;
    public static AlienGroupController instance;
    [SerializeField] float distanceBetweenAliens = 3f;

    public static event Action AllAliensGuessed;
    [SerializeField] Transform StartAlienGuessRowPoint;

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

    void OnEnable()
    {
        GameStateController.ChangeState += OnGameStateChenged;
    }

    private void OnDisable()
    {
        GameStateController.ChangeState -= OnGameStateChenged;
    }

    public void SetAliensPositionForGuessing()
    {
        _alienPositions = new List<Vector2>(); 

        Vector2 nextAlienPosition = StartAlienGuessRowPoint.transform.position;

        player_movement.instance.transform.position = nextAlienPosition + new Vector2(-distanceBetweenAliens, 0);

        for (int i = 0; i < aliens.Count; i++)
        {
            aliens[i].transform.position = nextAlienPosition;
            _alienPositions.Add(nextAlienPosition);

            nextAlienPosition.x += distanceBetweenAliens;
        }
        

        Physics2D.SyncTransforms();
    }

    public void OnGameStateChenged(GameState state)
    {
        switch (state)
        {
            case GameState.guessing:
            SetAliensPositionForGuessing();
            break;
            default:
            break;
        }
    }

    public void UpdateAliensPositionForGuessing()
    {
        if (aliens.Count == 0 )
        {
            AllAliensGuessed.Invoke();
            GameStateController.SetState(GameState.levelMenu);
        }

        for (int i = 0; i < aliens.Count; i++)
        {
            aliens[i].transform.position = _alienPositions[i];
        }

    }

    
}
