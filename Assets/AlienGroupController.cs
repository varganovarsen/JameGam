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

    public void SetAliensPositionForGuessing()
    {
        GameStateController.SetState(GameState.guessing);

        _alienPositions = new List<Vector2>(); 

        Vector2 nextAlienPosition = new Vector2(0, 0);

        player_movement.instance.transform.position = new Vector2(-distanceBetweenAliens, 0);

        for (int i = 0; i < aliens.Count; i++)
        {
            aliens[i].transform.position = nextAlienPosition;
            _alienPositions.Add(nextAlienPosition);

            nextAlienPosition.x += distanceBetweenAliens;
        }
        

        Physics2D.SyncTransforms();
    }

    public void UpdateAliensPositionForGuessing()
    {
        for (int i = 0; i < aliens.Count; i++)
        {
            aliens[i].transform.position = _alienPositions[i];
        }

    }

    



    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetAliensPositionForGuessing();
        }
    }

    
}
