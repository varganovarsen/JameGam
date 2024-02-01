using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AlienData : MonoBehaviour
{

    public string AlienName;
    public bool meeted = false;
    public Transform AlienMouthPoint;

    [SerializeField] float forgetNameTime = 10f;
    float _elapsedTime = 0;

    [SerializeField] public CanvasGroup guiLayoutGroup;

    [SerializeField] TMP_Text nameText;


    private void Start() {
        nameText.text = AlienName;
        guiLayoutGroup.alpha = 0;
        _elapsedTime = forgetNameTime;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        Mathf.Clamp(_elapsedTime, 0, forgetNameTime);
        guiLayoutGroup.alpha = Mathf.Lerp(1 , 0, _elapsedTime / forgetNameTime);
        
    }
    
    public void ResetVisibilityTime()
    {
        _elapsedTime = 0;
    }
    
}
