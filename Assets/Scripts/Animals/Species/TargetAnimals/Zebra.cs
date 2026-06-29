using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zebra : AnimalObjetivo
{
    enum EstadoZebra
    {
        esperando,
        corriendo, 
        golpeado, 
        muriendo
    }

    private readonly string[] idleAnimations =
    {
        "idleEat",
        "idleBreathe",
        "idleLookAround"
    };

    private Animation zebraAnim;

    private EstadoZebra estado;

    private float tiempo;
    private float frecuencia;
    private float amplitud;

    private void Awake()
    {
        zebraAnim = GetComponent<Animation>();
        tipoAnimal = TipoAnimal.Zebra;
    }

    protected override void Update()
    {
        base.Update();

        if (estado == EstadoZebra.muriendo)
            return; 

        if (estado == EstadoZebra.corriendo)
        {
            Moverse();
        }
    }


    public override void OnSpawn()
    {
        base.OnSpawn();

        vida = 2;

        tiempo = 0f;
        velocidad = Random.Range(1f, 10f);
        frecuencia = Random.Range(1f, 5f);
        amplitud = Random.Range(0.2f, 4f);

        transform.position = new Vector3(
            Random.Range(-10f, 50f),
            0f,
            Random.Range(-10f, 20f)
            );

        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        estado = EstadoZebra.esperando;
        StartCoroutine(Esperar());
    }

    IEnumerator Esperar()
    {
        string animacion = idleAnimations[Random.Range(0, idleAnimations.Length)];

        zebraAnim.Play(animacion);

        yield return new WaitForSeconds(Random.Range(1f, 3f));

        IniciarMovimiento();
    }

    void IniciarMovimiento()
    {
        estado = EstadoZebra.corriendo;

        if (velocidad < 1.5f)
        {
            zebraAnim.Play("walkEat");
        }
        else if (velocidad >= 1.5f && velocidad < 4f)
        {
            zebraAnim.Play("walk");
        }
        else if (velocidad >= 4f && velocidad < 6f)
        {
            zebraAnim.Play("trot");
        }
        else
        {
            zebraAnim.Play("galop");
        }
    }
    void Moverse()
    {
        tiempo += Time.deltaTime;

        Vector3 pos = transform.position;
        pos += transform.forward * velocidad * Time.deltaTime;

        pos += transform.right *
           Mathf.Sin(tiempo * frecuencia) *
           amplitud *
           Time.deltaTime;

        transform.position = pos;
    }

    public override void RecibirDisparo()
    {
        if (estado == EstadoZebra.golpeado || estado == EstadoZebra.muriendo)
            return;

        vida--;

        if (vida > 0)
        {
            StartCoroutine(RecibirGolpe());
            return;
        }

        estado = EstadoZebra.muriendo;
        Morir();
    }

    IEnumerator RecibirGolpe()
    {
        estado = EstadoZebra.golpeado;

        zebraAnim.Play("getHit");

        yield return new WaitForSeconds(zebraAnim["getHit"].length);

        velocidad = Random.Range(10f, 15f);
        IniciarMovimiento();
    }
    protected override IEnumerator MorirCoroutine()
    {
        zebraAnim.Stop();
        zebraAnim.Play("death");
        RegistrarMuerte();

        yield return new WaitForSeconds(zebraAnim["death"].length+tiempoMuerte);

        FactoryAnimalsPooling.Instance.ReturnObjectToPool(gameObject);
    }
}
