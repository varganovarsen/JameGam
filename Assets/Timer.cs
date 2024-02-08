using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;

    [SerializeField] private const float levelTime = 120f;
    private float remainTime = 0;
    bool stopTimer;

    [SerializeField] Transform circleToRotate;

    void Awake()
    {
        remainTime = levelTime;
    }

    private void Update()
    {
        if (!stopTimer)
        {
            remainTime-= Time.deltaTime;

            remainTime = Mathf.Clamp(remainTime - Time.deltaTime, 0, levelTime);
            timeText.text = Mathf.CeilToInt(remainTime).ToString();
            circleToRotate.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 180, (levelTime - remainTime) / levelTime));

            if (remainTime == 0)
            {
                stopTimer = true;
                GameStateController.SetState(GameState.guessing);
            }
        }
        
    }
}
