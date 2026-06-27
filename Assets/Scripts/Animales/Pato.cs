using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

enum EstadoPato
{
    nadando,
    caminando,
    muriendo
}

public class Pato : AnimalBehaviour
{
    public float radioOrbita;
    public float velocidadRotacion;
    public int sentido;
    private Vector3 centro;
    private float angulo;

    private EstadoPato estado;
    Animation patoAnim;

    private Vector3 previousPosition = Vector3.zero;

    private void Awake()
    {
        patoAnim = GetComponent<Animation>();
    }

    protected override void Update()
    {
        base.Update();

        if (estado == EstadoPato.muriendo)
            return;

        MoverseEnCirculos();
    }


    public override void OnSpawn()
    {
        base.OnSpawn();

        estado = Random.Range(0, 2) == 0 ? EstadoPato.nadando : EstadoPato.caminando;

        if (estado == EstadoPato.nadando)
        {
            centro = Centers.Instance.PatosCentroLago.position;
            centro.y = -0.1f;
            patoAnim.Play("swim");

        }
        else if (estado == EstadoPato.caminando)
        {
            centro = Centers.Instance.PatosCentroSuelo.position;
            centro.y = -0.05f;
            patoAnim.Play("walk");
        }

        transform.rotation = Quaternion.identity;
        previousPosition = transform.position;

        radioOrbita = Random.Range(2f, 10f);
        velocidadRotacion = Random.Range(5f, 30f);

        sentido = Random.Range(0, 2) == 0 ? -1 : 1;
        angulo = Random.Range(0f, 360f);
    }

    void MoverseEnCirculos()
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

    protected override IEnumerator MorirCoroutine()
    {
        estado = EstadoPato.muriendo;

        patoAnim.Stop();

        float giro = 0f;
        float velocidadGiro = 90f;


        while (giro < 90f)
        {
            float delta = velocidadGiro * Time.deltaTime;

            transform.Rotate(0f, 0f, delta);

            giro += delta;

            transform.position = new Vector3(
            transform.position.x,
            0.1f,
            transform.position.z
        );

            yield return null;
        }

        yield return new WaitForSeconds(tiempoMuerte);

        FactoryObjectPooling.Instance.ReturnObjectToPool(gameObject);
    }

}
