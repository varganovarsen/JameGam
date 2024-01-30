using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AlienSpriteGeneration : MonoBehaviour
{
    List<GameObject> bodyPrefabs;
    List<GameObject> headPrefabs;
    List<GameObject> facePrefabs;

    private void Start()
    {
        BodypartsPrefabsLoad();
    }

    private void BodypartsPrefabsLoad()
    {
        bodyPrefabs = new List<GameObject>();
        var allBodyPrefs = Resources.LoadAll<GameObject>("body").Cast<GameObject>();
        foreach (var pref in allBodyPrefs)
        {
            bodyPrefabs.Add(pref);
            Debug.Log(pref.name + " loaded");
        }

        headPrefabs = new List<GameObject>();
        var allHeadPrefs = Resources.LoadAll<GameObject>("head").Cast<GameObject>();
        foreach (var pref in allHeadPrefs)
        {
            headPrefabs.Add(pref);
            Debug.Log(pref.name + " loaded");
        }

        facePrefabs = new List<GameObject>();
        var allFacePrefs = Resources.LoadAll<GameObject>("face").Cast<GameObject>();
        foreach (var pref in allFacePrefs)
        {
            facePrefabs.Add(pref);
            Debug.Log(pref.name + " loaded");
        }
    }

    public void GenerateSprite()
    {
        //generating body
        
        
    
     }

    private void OnDisable()
    {
        Resources.UnloadUnusedAssets();
    }
}
