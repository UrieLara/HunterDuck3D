using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalType
{
    Objetivo, 
    Salvaje
}
[System.Serializable] 
public class AnimalPool
{
    public string animalName;
    public GameObject animalPrefab;
    public int poolSize = 5; 
    public float spawnTime = 5f;

    public AnimalType type;

    [HideInInspector]
    public List<GameObject> poolObjects = new();
}
