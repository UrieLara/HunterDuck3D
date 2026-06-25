using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

enum PatoType
{
    Nadador,
    Caminador
}
public class Pato : AnimalBehaviour
{
    public float radioOrbita;
    public float velocidadRotacion;
    public int sentido;
    private Vector3 centro;
    private float angulo;

    private PatoType tipoPato;
    Animation patoAnimation;

    private Vector3 previousPosition;
    private bool spawned;

    private void Awake()
    {
        patoAnimation = GetComponent<Animation>();
    }

    void Update()
    {
        if (!spawned)
            return;

        MoveInCircles();
    }


    public override void OnSpawn()
    {
        spawned = true;

        tipoPato = (PatoType)Random.Range(0, 2);

        if (tipoPato == PatoType.Nadador)
        {
            centro = Centers.Instance.PatosCentroLago.position;
           
        }
        else if (tipoPato == PatoType.Caminador)
        {
            centro = Centers.Instance.PatosCentroSuelo.position;
        }

        centro.y = -0.1f;
        previousPosition = transform.position;

        radioOrbita = Random.Range(2f, 10f);
        velocidadRotacion = Random.Range(20f, 40f);

        sentido = Random.Range(0, 2) == 0 ? -1 : 1;

        angulo = Random.Range(0f, 360f);
    }

    void MoveInCircles()
    {
        float x = Mathf.Cos(angulo * Mathf.Deg2Rad) * radioOrbita;
        float z = Mathf.Sin(angulo * Mathf.Deg2Rad) * radioOrbita;

        transform.position = centro + new Vector3(x, 0f, z);

        Vector3 direccion = new Vector3(
        -Mathf.Sin(angulo * Mathf.Deg2Rad),
         0f,
         Mathf.Cos(angulo * Mathf.Deg2Rad)
        );

        direccion *= sentido;

        transform.rotation =
            Quaternion.LookRotation(direccion) *
            Quaternion.Euler(0, 180, 0);


        angulo += velocidadRotacion * sentido * Time.deltaTime;
    }

}
