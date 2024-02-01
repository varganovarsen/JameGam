using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NameUiDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler
{

    Vector2 _startDragPosition;
    bool isAboveAlien = false;
    Transform trueParent;
 
    private void Start()
    {
        trueParent = transform.parent;
        this.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startDragPosition = transform.localPosition;
        transform.SetParent(GuestGuessController.instance.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        transform.SetParent(trueParent);
        transform.localPosition = _startDragPosition;
    }
}

