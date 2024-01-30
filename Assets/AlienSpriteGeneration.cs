using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class AlienSpriteGeneration : MonoBehaviour
{
    [SerializeField, Min(1)] private int aliensToGenerate;
    [SerializeField] GameObject alienPrefab;
    [SerializeField] Vector2 spawnRandom;
    
    List<GameObject> bodyPrefabs;
    List<GameObject> headPrefabs;
    List<GameObject> facePrefabs;

    private void Start()
    {
        BodypartsPrefabsLoad();


        for (int i = 0; i < aliensToGenerate; i++)
        {
            GenerateAlien();
        }
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


    public void GenerateAlien()
    {
        Vector2 position = new Vector2(Random.Range(-spawnRandom.x, spawnRandom.x),
            Random.Range(-spawnRandom.y, spawnRandom.y));

        GameObject alien = Instantiate(alienPrefab, position, Quaternion.identity);
        
        GenerateSprite(alien.transform);
    }

    public void GenerateSprite(Transform parentObject)
    {
        //generating body
        GameObject body = Instantiate(bodyPrefabs[Random.Range(0, bodyPrefabs.Count)], parentObject);
        
        //generating head
        GameObject head = Instantiate(headPrefabs[Random.Range(0, headPrefabs.Count)], body.transform.GetChild(0));
    
        //generating face
        GameObject face = Instantiate(facePrefabs[Random.Range(0, facePrefabs.Count)], head.transform.GetChild(0));
    }

    private void OnDisable()
    {
        Resources.UnloadUnusedAssets();
    }
}
