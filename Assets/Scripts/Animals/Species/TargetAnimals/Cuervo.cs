using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EstadoCuervo
{
    esperando,
    despegando,
    volando, 
    muriendo
}

public class Cuervo : AnimalObjetivo
{
    private Animator cuervoAnim;

    private EstadoCuervo estado;

    public float velocidadDespegue = 5f;
    public float velocidadVuelo = 15f;
    public float alturaVuelo = 6f;

    private float tiempo;
    private float frecuencia;
    private float amplitud;


    private void Awake()
    {
        cuervoAnim = GetComponent<Animator>();
        tipoAnimal = TipoAnimal.Cuervo;
    }

    protected override void Update()   
    {
        base.Update();

        if (estado == EstadoCuervo.muriendo)
            return;

        switch (estado)
        {
            case EstadoCuervo.despegando:
                Despegar();
                break;

            case EstadoCuervo.volando:
                Volar();
                break;
        }

        ComprobarLimites();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        estado = EstadoCuervo.esperando;

        tiempo = 0f;
        frecuencia = Random.Range(1f, 5f);
        amplitud = Random.Range(0.2f, 1f);

        transform.position = new Vector3(
            Random.Range(-10f, 50f),
            0f, 
            Random.Range(-10f, 20f)
            );

        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        StartCoroutine(Esperar());
        
    }

    IEnumerator Esperar()
    {
        cuervoAnim.SetBool("flying", false);

        yield return new WaitForSeconds(Random.Range(2f, 5f));

        transform.rotation = Quaternion.Euler(-20f, transform.eulerAngles.y, 0f);

        cuervoAnim.SetBool("flying", true);
        estado = EstadoCuervo.despegando;
    }

    void Despegar()
    {
        Vector3 movimiento =
            transform.forward +
            Vector3.up;

        transform.position +=
            movimiento.normalized *
            velocidadDespegue *
            Time.deltaTime;

        if (transform.position.y >= alturaVuelo)
        {
            estado = EstadoCuervo.volando;

            transform.rotation =
                Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        }
    }

    void Volar()
    {
        tiempo += Time.deltaTime;

        Vector3 pos = transform.position;
        pos += transform.forward * velocidadVuelo * Time.deltaTime;
        pos.y = alturaVuelo + Mathf.Sin(tiempo * frecuencia) * amplitud;
        transform.position = pos;
    }

    protected override IEnumerator MorirCoroutine()
    {
        estado = EstadoCuervo.muriendo;

        RegistrarMuerte();
        cuervoAnim.SetBool("flying", false);
        cuervoAnim.Play("die");

        while (transform.position.y > 0.05f)
        {
            transform.position += Vector3.down * 8f * Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(
            transform.position.x,
            0f,
            transform.position.z
        );

        yield return new WaitForSeconds(tiempoMuerte);

        FactoryAnimalsPooling.Instance.ReturnObjectToPool(gameObject);
    }

}
