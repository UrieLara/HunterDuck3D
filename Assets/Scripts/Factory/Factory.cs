using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{

    public GameObject pato;
    public GameObject cuervo; 
    public GameObject gallina;


    [Header("Tiempo")]
    [SerializeField] private float spawnInterval = 5f;
    void Start()
    {
        StartCoroutine(SpawnCuervo());
        StartCoroutine(SpawnGallina());
        StartCoroutine(SpawnPato());

    }
    IEnumerator SpawnCuervo()
    {
        while (true)
        {
           Vector3 randomOffset = new Vector3(Random.Range(-30f, 70f), Random.Range(2f, 6f), Random.Range(-50f,3f));
           GameObject ave = Instantiate(cuervo, cuervo.transform.position + randomOffset, cuervo.transform.rotation);
            Destroy(ave, 20f);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnGallina()
    {
        while (true)
        {
            Vector3 randomOffset = new Vector3(-25f, 0.5f, Random.Range(-10f, 10f));
            GameObject ave = Instantiate(gallina, gallina.transform.position + randomOffset, gallina.transform.rotation);
            Destroy(ave, 20f);
            yield return new WaitForSeconds(spawnInterval+10f);
        }
    }

    IEnumerator SpawnPato()
    {
        while (true)
        {
            GameObject ave = Instantiate(pato, pato.transform.position, pato.transform.rotation);
            Destroy(ave, 20f);
            yield return new WaitForSeconds(spawnInterval+20f);
        }
    }

}
