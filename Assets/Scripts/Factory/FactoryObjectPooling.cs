using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryObjectPooling : MonoBehaviour
{
    public static FactoryObjectPooling Instance;
    public List<AnimalPool> animals = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        foreach (var animalPool in animals)
        {
           InitializePool(animalPool);
           StartCoroutine(SpawnAnimal(animalPool));
        }
    }

    void InitializePool(AnimalPool animal)
    {
        for (int i = 0; i < animal.poolSize; i++)
        {
            GameObject obj = Instantiate(animal.animalPrefab);
            obj.SetActive(false);
            animal.poolObjects.Add(obj);
        }
    }

    IEnumerator SpawnAnimal(AnimalPool animalPool)
    {
        while (true)
        {
            GameObject obj = GetObjectFromPool(animalPool.poolObjects);

            if (obj != null)
            {
                 obj.SetActive(true);

                AnimalBehaviour animal = obj.GetComponent<AnimalBehaviour>();
                if (animal != null)
                {
                    animal.OnSpawn();
                }
              
            }

            yield return new WaitForSeconds(animalPool.spawnTime);
        }
    }

    GameObject GetObjectFromPool(List<GameObject> poolObjects)
    {
        foreach (GameObject obj in poolObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        AnimalBehaviour animal = obj.GetComponent<AnimalBehaviour>();
        if (animal != null)
        {
            animal.OnDespawn();
        }

        obj.SetActive(false);
    }

}
