using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DEBUG_currentAlienText : MonoBehaviour
{
    static TMP_Text currentAlienText;
    private void Start()
    {
        currentAlienText = GetComponent<TMP_Text>();
    }

    public static void UpdateText(string updateTo)
    {
        currentAlienText.text = updateTo;
    }
}
