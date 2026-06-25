using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryObjectPooling : MonoBehaviour
{
    public static FactoryObjectPooling Instance;
    public List<AnimalPool> animals = new();

    // [Header("Objetivos")]
    //public GameObject patoPrefab;
    //public GameObject cuervoPrefab; 
    //public GameObject zebraPrefab;

    // [Header("Salvajes")]
    // public GameObject rinoPrefab;
    // public GameObject lionPrefab;

    // [Header("Tiempos")]
    // public float objectDisableTime = 20f;
    // [Header("Objetivos")]
    // public float patoTime = 10f;
    // public float cuervoTime = 5f;
    // public float zebraTime = 5f;

    // [Header("Salvajes")]
    // public float rinoTime = 5f;
    // public float lionTime = 5f;


    // [Header("Pool Size")]
    // [Header("Objetivos")]
    // public int patoPool = 10;
    // public int cuervoPool = 5;
    // public int zebraPool = 5;

    // [Header("Salvajes")]
    // public int rinoPool = 5;
    // public int lionPool = 5;


    // private List<GameObject> patoPool = new List<GameObject>();
    // private List<GameObject> cuervoPool = new List<GameObject>();
    // private List<GameObject> zebraPool = new List<GameObject>();

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
        //InitializePool(patoPrefab, patoPool, poolSize);
        //InitializePool(cuervoPrefab, cuervoPool, poolSize);
        //InitializePool(zebraPrefab, zebraPool, poolSize);

        //StartCoroutine(SpawnCuervo());
        //StartCoroutine(SpawnGallina());
        //StartCoroutine(SpawnPato());
    }

    void InitializePool(AnimalPool animal)
    {
        for (int i = 0; i < animal.poolSize; i++)
        {
            GameObject obj = Instantiate(animal.animalPrefab);
            obj.SetActive(false);
            animal.poolObjects.Add(obj);
        }

        //for (int i = 0; i < poolSize; i++)
        //{
        //    GameObject obj = Instantiate(prefab);
        //    obj.SetActive(false);
        //    poolObjects.Add(obj);
        //}
    }

    //IEnumerator SpawnAnimal(AnimalPool animal)
    //{
    //    while (true)
    //    {
    //        GameObject obj = GetObjectFromPool(animal.poolObjects);
    //        if (obj != null)
    //        {
    //            Vector3 randomOffset = new Vector3(Random.Range(-30f, 70f), Random.Range(2f, 6f), Random.Range(-50f, 3f));
    //            obj.transform.position = animal.animalPrefab.transform.position + randomOffset;
    //            obj.transform.rotation = animal.animalPrefab.transform.rotation;
    //            StartCoroutine(DisableAfterTime(obj, animal.spawnTime));
    //        }
    //        yield return new WaitForSeconds(animal.spawnTime);
    //    }
    //}

    //GameObject GetObjectFromPool(List<GameObject> poolObjects)
    //{
    //    foreach (GameObject obj in poolObjects)
    //    {
    //        if (!obj.activeInHierarchy)
    //        {
    //            obj.SetActive(true);
    //            return obj;
    //        }
    //    }
    //    return null;
    //}

    //public void ReturnObjectToPool(GameObject obj)
    //{
    //    obj.SetActive(false);
    //}

    //IEnumerator DisableAfterTime(GameObject obj, float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    obj.SetActive(false);
    //}

    //IEnumerator SpawnPato()
    //{
    //    while (true)
    //    {
    //        GameObject pato = GetObjectFromPool(patoPool);

    //        if (pato != null)
    //        {
    //            pato.transform.position = patoPrefab.transform.position;
    //            pato.transform.rotation = patoPrefab.transform.rotation;
    //            StartCoroutine(DisableAfterTime(pato, objectDisableTime));
    //        }
    //        yield return new WaitForSeconds(patoTime);
    //    }
    //}

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
