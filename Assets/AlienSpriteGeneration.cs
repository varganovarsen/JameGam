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
    NameBank _nameBank;

    List<GameObject> bodyPrefabs;
    List<GameObject> headPrefabs;
    List<GameObject> facePrefabs;

    [SerializeField] private List<Material> spriteMaterials;

    [SerializeField] private GoogleSheetLoader _loader;

    private void Awake()
    {
        _loader.OnProcessData += StartGenerating;
    }

    private void Start()
    {
        BodypartsPrefabsLoad();

    }

    public void StartGenerating(NameBank names)
    {
        _nameBank = names;
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
        AlienData _alienData;
        alien.TryGetComponent<AlienData>(out _alienData);

        if (!_alienData)
        {
            Debug.Log("There is no AlienData component attached to " + alien + " object");
            return;
        }

        int nameID = Random.Range(0, _nameBank.Names.Count);
        _alienData.AlienName = _nameBank.Names[nameID];
        _nameBank.Names.Remove(_alienData.AlienName);
        _alienData.AlienMouthPoint = GenerateSprite(alien.transform);
    }

    public Transform GenerateSprite(Transform parentObject)
    {
        
        Material mat = spriteMaterials[Random.Range(0, spriteMaterials.Count)];
        //generating body
        GameObject body = Instantiate(bodyPrefabs[Random.Range(0, bodyPrefabs.Count)], parentObject);

        body.GetComponentInChildren<SpriteRenderer>().material = mat;
        //generating head
        GameObject head = Instantiate(headPrefabs[Random.Range(0, headPrefabs.Count)], body.transform.GetChild(0));

        head.GetComponentInChildren<SpriteRenderer>().material = mat;
        //generating face
        GameObject face = Instantiate(facePrefabs[Random.Range(0, facePrefabs.Count)], head.transform.GetChild(0));

        face.GetComponentInChildren<SpriteRenderer>().material = mat;

        return face.transform.GetChild(0);
    }

    private void OnDisable()
    {
        Resources.UnloadUnusedAssets();
    }
}
