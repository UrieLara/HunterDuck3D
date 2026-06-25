using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Pato : Ave
{
    Vector3 centro = new Vector3(-26f, 0.1f, -33f);
    public float radioDeOrbita = 0;
    int sentido = 1;

    private void Start()
    {
        radioDeOrbita = Random.Range(1f, 10f);
        transform.position = centro + new Vector3(radioDeOrbita, 0f, radioDeOrbita);
        velocidad = Random.Range(10f, 20f);
        sentido = Random.Range(0, 2) == 0 ? -1 : 1;

    }
    void Update()
    {
        Caminar();
    }
    protected override void Caminar()
    {
        transform.RotateAround(centro, Vector3.up, sentido * velocidad * Time.deltaTime);
    }
}
