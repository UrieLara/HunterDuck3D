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

    IEnumerator SpawnAnimal(AnimalPool animal)
    {
        while (true)
        {
            GameObject obj = GetObjectFromPool(animal.poolObjects);

            if (obj != null)
            {
                 obj.SetActive(true);

                AnimalBehaviour comportamiento = obj.GetComponent<AnimalBehaviour>();
                if (comportamiento != null)
                {
                    comportamiento.OnSpawn();
                }

                StartCoroutine(DisableAfterTime(obj, animal.disableTime));
            }

            yield return new WaitForSeconds(animal.spawnTime);
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
        obj.SetActive(false);
    }

    IEnumerator DisableAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }


    //IEnumerator SpawnZebra()
    //{
    //    while (true)
    //    {
    //        GameObject zebra = GetObjectFromPool(zebraPool);
    //        Vector3 randomOffset = new Vector3(-25f, 0.5f, Random.Range(-10f, 10f));

    //        if (zebra != null)
    //        {
    //            zebra.transform.position = zebraPrefab.transform.position + randomOffset;
    //            zebra.transform.rotation = zebraPrefab.transform.rotation;
    //            StartCoroutine(DisableAfterTime(zebra, objectDisableTime * 2));
    //        }
    //        yield return new WaitForSeconds(zebraTime);
    //    }
    //}

    //IEnumerator SpawnCuervo()
    //{
    //    while (true)
    //    {
    //        GameObject cuervo = GetObjectFromPool(cuervoPool);
    //        Vector3 randomOffset = new Vector3(Random.Range(-30f, 70f), Random.Range(2f, 6f), Random.Range(-50f, 3f));
    //        if (cuervo != null)
    //        {
    //            cuervo.transform.position = cuervoPrefab.transform.position + randomOffset;
    //            cuervo.transform.rotation = cuervoPrefab.transform.rotation;
    //            StartCoroutine(DisableAfterTime(cuervo, objectDisableTime));
    //        }
    //        yield return new WaitForSeconds(cuervoTime);
    //    }
    //}

}
