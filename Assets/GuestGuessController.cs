using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuestGuessController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isOpen;
    public List<AlienData> meetedGuests;
    public static GuestGuessController instance;

    [SerializeField] private Transform IsOpenTransform;
    [SerializeField] private Transform IsClosedTransform;

    [SerializeField] private GameObject nameInListUIPrefab;

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
    }


    public void MeetGuest(AlienData guest){
        meetedGuests.Add(guest);
        AddGuestToListUI(guest);

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
                ToggleMenuOpen(true);
                ignoreMouse = true;
                GetComponentInChildren<GridLayoutGroup>().enabled = false;

                Transform names = transform.Find("names");

                for (int i = 0; i < names.childCount; i++)
                {
                    names.GetChild(i).GetComponent<NameUiDrag>().enabled = true;
                }
                
            break;
            default:
            ignoreMouse = false;
            break;
        }

    }
}
