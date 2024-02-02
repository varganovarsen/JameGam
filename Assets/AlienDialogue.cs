using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDialogue : MonoBehaviour
{
    [SerializeField] public static float startDialogueDistance = 3.5f;
    

    bool canStartDialogue = false;

    AlienData _data;

    /// <summary>
    /// Called when the mouse enters the GUIElement or Collider.
    /// </summary>
    private void OnMouseEnter()
    {
        NameUiDrag.CurrentAlien = _data;
    }

    private void OnMouseOver() {

        if (Vector2.Distance(player_movement.instance.transform.position, transform.position) < startDialogueDistance)
        {
            canStartDialogue = true;
        }
    }

    
    private void OnMouseExit()
    {
        NameUiDrag.CurrentAlien = null;
        canStartDialogue = false;
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    private void OnMouseDown()
    {
        if (canStartDialogue && GameStateController.CanAct)
        {
            if (!_data.meeted)
            {
                Meet();
            }
        }
    }

    private void Start()
    {
        _data = GetComponent<AlienData>();
    }

    public void Meet()
    {
        _data.meeted = true;

        //TODO добавление имени в список имён
        GuestGuessController.instance.MeetGuest(_data);
        //TODO Обновить таймер исчезнования имени
        _data.ResetVisibilityTime();
    }
}
