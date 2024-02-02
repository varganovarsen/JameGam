using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NameUiDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler
{

    Vector2 _startDragPosition;
    Transform trueParent;
    CanvasGroup _canvasGroup;

    [SerializeField] LayerMask alienLayer;

    private static AlienData currentAlien;
    List<Vector2> nameDrops;

    public static AlienData CurrentAlien {
         get => currentAlien;
         set {
            if (value)
                DEBUG_currentAlienText.UpdateText(value.AlienName);    
            else
                DEBUG_currentAlienText.UpdateText("Null");

            currentAlien = value;
            
            } }

    private void Start()
    {
        trueParent = transform.parent;
        nameDrops = new List<Vector2>();
        _canvasGroup = GetComponent<CanvasGroup>();
        this.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startDragPosition = transform.localPosition;
        
        //GetComponent<TMP_Text>().raycastTarget = false;
        transform.SetParent(GuestGuessController.instance.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {        
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {

        Debug.Log("Name drop");
        AlienData _data = null;

        Vector3 posToCheck = Camera.main.ScreenToWorldPoint(transform.position);

        nameDrops.Add(posToCheck);


        List<Collider2D> col = new List<Collider2D>();
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.layerMask = alienLayer;
        filter2D.ClearDepth();
        filter2D.useLayerMask = true;

        Physics2D.OverlapPoint(posToCheck, filter2D, col);
        
        if (col.Count == 0)
        {
            ResetPosition();
            return;
        }
        
        foreach (var C in col)
        {
            C.TryGetComponent<AlienData>(out _data);
            Debug.Log(C.name);
            if (_data)
            break;
        }

        bool isAboveAlien = _data != null;


        //GetComponent<TMP_Text>().raycastTarget = true;

        if (!isAboveAlien)
        {
            ResetPosition();
            return;
        }

        
        if (_data.Guess(GetComponent<TMP_Text>().text))
        {
            Destroy(gameObject);
        } else
        {
            ResetPosition();
        }
    }

    void ResetPosition(){
        transform.SetParent(trueParent);
        transform.localPosition = _startDragPosition;
        
    }

    
    private void OnDrawGizmos()
    {
        foreach (var dropPos in nameDrops)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(dropPos, 0.1f);
        }
    }
}

