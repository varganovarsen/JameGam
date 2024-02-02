using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AlienData : MonoBehaviour
{
    private const float SecondsBeforeDelete = 4f;
    public string AlienName;
    public bool meeted = false;
    public Transform AlienMouthPoint;

    [SerializeField] float forgetNameTime = 10f;
    float _elapsedTime = 0;

    bool forgettingName = true;

    [SerializeField] public CanvasGroup _canvasGroup;

    [SerializeField] TMP_Text nameText;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        GameStateController.ChangeState += OnStateChanged;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        GameStateController.ChangeState -= OnStateChanged;
    }


private void Start() {
        nameText.text = AlienName;
        gameObject.name = AlienName;
        _canvasGroup.alpha = 0;
        _elapsedTime = forgetNameTime;
        


        AlienGroupController.instance.aliens.Add(this);
    }

    private void Update()
    {

        if (forgettingName)
        {
            _elapsedTime += Time.deltaTime;
            Mathf.Clamp(_elapsedTime, 0, forgetNameTime);
            _canvasGroup.alpha = Mathf.Lerp(1 , 0, _elapsedTime / forgetNameTime);
        }
        
    }
    
    public void ResetVisibilityTime()
    {
        _elapsedTime = 0;
    }

    public void OnStateChanged(GameState gameState)
    {
        switch (gameState)
        {   
            case GameState.guessing:
                OnGuessingState();
            break;
            default:

            break;
        }
    }

    public void OnGuessingState()
    {
        forgettingName = false;
        _canvasGroup.alpha = 0f;
    }

    public bool Guess(string guessedName)
    {
        _canvasGroup.alpha = 1f;

        bool _guessIsRight = guessedName == AlienName;
        nameText.color = _guessIsRight ? Color.green : Color.red;
        if (_guessIsRight)
            GuestGuessController.instance.levelGuessData._rightGuessedAliens++;
        
        IEnumerator cor = DeleteAlien();
        StartCoroutine(cor);

        return _guessIsRight;
    }

    public IEnumerator DeleteAlien()
    {   

        yield return new WaitForSecondsRealtime(SecondsBeforeDelete);
        
        AlienGroupController.instance.aliens.Remove(this);
        AlienGroupController.instance.UpdateAliensPositionForGuessing();
        Destroy(gameObject);    
    }
    
}
