using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Rino : AnimalSalvaje
{
    Animation rinoAnim;

    private void Awake()
    {
        rinoAnim = GetComponent<Animation>();
    }
    protected override void Update()
    {
        base.Update();

        switch (estado)
        {
            case EstadoSalvaje.esperando:
                Esperar();
                break;

            case EstadoSalvaje.atacando:
                Correr();
                break;

        }
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        vida = 2;
        velocidad = 20f;
    }

    void Esperar()
    {
        rinoAnim.Play("walk");

        transform.position += transform.forward * 1f * Time.deltaTime;
    }

    void Correr()
    {
        rinoAnim.Play("run");

        transform.position += transform.forward * velocidad * Time.deltaTime;
    }

    protected override IEnumerator RecibirGolpe()
    {
        estado = EstadoSalvaje.golpeado;

        rinoAnim.Play("getHit");

        yield return new WaitForSeconds(rinoAnim["getHit"].length);

        estado = EstadoSalvaje.atacando;

    }

    protected override IEnumerator AnimAttack()
    {
        rinoAnim.Play("walk2HitAttack");
        CameraEffects.Instance.DamageEffect();

        yield return new WaitForSeconds(rinoAnim["walk2HitAttack"].length);

        rinoAnim.Play("run");
    }
    protected override IEnumerator MorirCoroutine()
    {
        estado = EstadoSalvaje.muriendo;

        rinoAnim.Stop();
        rinoAnim.Play("death");

        yield return new WaitForSeconds(rinoAnim["death"].length + tiempoMuerte);

        FactoryAnimalsPooling.Instance.ReturnObjectToPool(gameObject);
    }

}
