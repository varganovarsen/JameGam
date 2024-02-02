using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuestGuessController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isOpen;
    public List<AlienData> meetedGuests;
    public List<AlienData> notMeetedGuests;
    public static GuestGuessController instance;
    [SerializeField] public LevelGuessData levelGuessData;

    [SerializeField] private Transform IsOpenTransform;
    [SerializeField] private Transform IsClosedTransform;

    [SerializeField] private GameObject nameInListUIPrefab;
    [SerializeField] private GameObject CantRememberGO;

    [SerializeField] public const float pointsForGuess = 5f;
    [SerializeField] public const float pointsForMeet = 1f;
    [SerializeField] public const float pointsForDifferentName = -3f;
    [SerializeField] public const float pointsForCantRememberName = -0.5f;

    bool ignoreMouse = false;

    private void Awake() {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        GameStateController.ChangeState += OnStateChanged;
    }
    private void OnDisable()
    {
        GameStateController.ChangeState -= OnStateChanged;  
    }

    private void Start(){
        ToggleMenuOpen(false);
        levelGuessData = new LevelGuessData();
        CantRememberGO.GetComponent<CanvasGroup>().alpha = 0f;;
    }


    public void MeetGuest(AlienData guest){
        meetedGuests.Add(guest);
        AddGuestToListUI(guest);
        levelGuessData._meetedAliens++;

    }



    private void AddGuestToListUI(AlienData guest)
    {
        
        GameObject nameUI = Instantiate(nameInListUIPrefab, transform.Find("names"));
        nameUI.GetComponent<TMP_Text>().text = guest.AlienName;
    }

    void ToggleMenuOpen()
    {

        isOpen = !isOpen;

        transform.position = isOpen ? IsOpenTransform.position : IsClosedTransform.position;
        transform.rotation = isOpen ? IsOpenTransform.rotation : IsClosedTransform.rotation;
    }

    void ToggleMenuOpen(bool setTo)
    {

        isOpen = setTo;

        transform.position = isOpen ? IsOpenTransform.position : IsClosedTransform.position;
        transform.rotation = isOpen ? IsOpenTransform.rotation : IsClosedTransform.rotation;
    }

   public  void OnPointerEnter(PointerEventData data)
    {
        
        if(!ignoreMouse)
        ToggleMenuOpen();
    }

    public  void OnPointerExit(PointerEventData data)
    {
        if(!ignoreMouse)
        ToggleMenuOpen();
    }

    public void OnStateChanged(GameState state)
    {
        switch (state)
        {   
            case GameState.guessing:
                levelGuessData._aliensCount = AlienGroupController.instance.aliens.Count;

                CantRememberGO.GetComponent<CanvasGroup>().alpha = 1f;
                CantRememberGO.GetComponent<NameUiDrag>().enabled = true;

                notMeetedGuests = new List<AlienData>(AlienGroupController.instance.aliens);

                for (int i = 0; i < notMeetedGuests.Count; i++)
                {
                    if(meetedGuests.Contains(notMeetedGuests[i]))
                    {
                        notMeetedGuests.RemoveAt(i);
                    }
                }

                levelGuessData._notMeetedAliens = notMeetedGuests.Count;

                ToggleMenuOpen(true);
                ignoreMouse = true;
                GetComponentInChildren<GridLayoutGroup>().enabled = false;

                Transform names = transform.Find("names");

                for (int i = 0; i < names.childCount; i++)
                {
                    names.GetChild(i).GetComponent<NameUiDrag>().enabled = true;
                }
                
            break;
            case GameState.levelMenu:

                ToggleMenuOpen(true);
                ignoreMouse = true;

                GetComponentInChildren<GridLayoutGroup>().enabled = true;

                CantRememberGO.GetComponent<CanvasGroup>().alpha = 0f;
                CantRememberGO.GetComponent<NameUiDrag>().enabled = false;

                foreach (var guest in notMeetedGuests)
                {
                    AddGuestToListUI(guest);
                }
            break;
            default:
            ignoreMouse = false;
            break;
        }

    }
}

[Serializable]
public struct LevelGuessData
{
    public float _aliensCount;
    public float _meetedAliens;
    public float _rightGuessedAliens;
    public float _totalPoints
    {
        get
        {
            return CalculatePoints();
        }
        private set{ _totalPoints = value;}
    }

    public float _notMeetedAliens;

    public LevelGuessData(float aliensCount, float meetedAliens, float rightGuessedAliens, float notMeetedAliens)
    {
        _aliensCount = aliensCount;
        _meetedAliens = meetedAliens;
        _rightGuessedAliens = rightGuessedAliens;
        _notMeetedAliens = notMeetedAliens;

        _totalPoints = 0;
        _totalPoints = CalculatePoints();
        
    }

    float CalculatePoints()
    {
        return
            (GuestGuessController.pointsForMeet * _meetedAliens) + 
            (GuestGuessController.pointsForGuess * _rightGuessedAliens)
            
            // reduce points for cant remember and wrong guess
            ;
    }

    

}
