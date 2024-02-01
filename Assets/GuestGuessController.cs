using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GuestGuessController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isOpen;
    public List<AlienData> meetedGuests;
    public static GuestGuessController instance;

    [SerializeField] private Transform IsOpenTransform;
    [SerializeField] private Transform IsClosedTransform;

    [SerializeField] private GameObject nameInListUIPrefab;

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

    private void Start(){
        ToggleMenuOpen(false);
    }


    public void MeetGuest(AlienData guest){
        meetedGuests.Add(guest);
        AddGuestToListUI(guest);

    }


    
  
    private void OnMouseEnter()
    {
        
    }

    
    private void OnMouseExit()
    {
        
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
        Debug.Log("OnPointerEnter called.");
        ToggleMenuOpen();
    }

    public  void OnPointerExit(PointerEventData data)
    {
        Debug.Log("OnPointerExit called.");
        ToggleMenuOpen();
    }
}
