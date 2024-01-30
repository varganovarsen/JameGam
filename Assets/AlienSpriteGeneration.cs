using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpriteGeneration : MonoBehaviour
{
    [SerializeField] private string bodyPrefabPath;
    [SerializeField] private int bodyCount;
    List<GameObject> bodyPrefabs;
    [SerializeField] private string headPrefabPath;
    [SerializeField] private int headCount;
    List<GameObject> headPrefabs;
    [SerializeField] private string facePrefabPath;
    [SerializeField] private int faceCount;
    List<GameObject> facePrefabs;

    private void Start()
    {
        BodypartsPrefabsLoad();
    }

    private void BodypartsPrefabsLoad()
    {
        bodyPrefabs = new List<GameObject>();
        for (int i = 1; i == bodyCount; i++)
        {
            string path = bodyPrefabPath + i.ToString();
            GameObject bodyPrefab = Resources.Load<GameObject>(path) as GameObject;
            bodyPrefabs.Add(bodyPrefab);
        }
        
        headPrefabs = new List<GameObject>();
        for (int i = 1; i == headCount; i++)
        {
            string path = headPrefabPath + i.ToString();
            GameObject headPrefab = Resources.Load<GameObject>(path) as GameObject;
            headPrefabs.Add(headPrefab);
        }
        
        facePrefabs = new List<GameObject>();
        for (int i = 1; i == faceCount; i++)
        {
            string path = facePrefabPath + i.ToString();
            GameObject facePrefab = Resources.Load<GameObject>(path) as GameObject;
            facePrefabs.Add(facePrefab);
        }
    }

    public void GenerateSprite()
    {
        //generating body
        
        
    
     }
    
    
   
  
}
