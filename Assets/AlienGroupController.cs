using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class AlienGroupController : MonoBehaviour
{

    public List<AlienData> aliens;
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

        Vector2 nextAlienPosition = new Vector2(0, 0);

        player_movement.instance.transform.position = new Vector2(-distanceBetweenAliens, 0);

        foreach (AlienData alien in aliens)
        {
            alien.transform.position = nextAlienPosition;
            nextAlienPosition.x += distanceBetweenAliens;
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
