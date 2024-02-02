using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] TMP_Text aliensCountText;
    [SerializeField] TMP_Text meetedAliensText;
    [SerializeField] TMP_Text rightGuessedAliensText;
    [SerializeField] TMP_Text totalPointText;

    CanvasGroup canvasGroup;
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        AlienGroupController.AllAliensGuessed += OpenLevelMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenLevelMenu()
    {
        
       ApplyLevelData(GuestGuessController.instance.levelGuessData);
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void ApplyLevelData(LevelGuessData _data)
    {
        aliensCountText.text = _data._aliensCount.ToString();
        meetedAliensText.text = _data._meetedAliens.ToString();
        rightGuessedAliensText.text = _data._rightGuessedAliens.ToString();
        totalPointText.text = _data._totalPoints.ToString();
    }
}
