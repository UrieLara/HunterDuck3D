using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class AnimalPool
{
    public string animalName;
    public GameObject animalPrefab;
    public int poolSize = 5; 
    public float spawnTime = 5f;
    public float initialDelay = 0f;

    [HideInInspector]
    public List<GameObject> poolObjects = new();
}
