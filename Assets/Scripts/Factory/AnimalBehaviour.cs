using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalBehaviour : MonoBehaviour
{
    protected bool spawned;
    protected int vida;

    protected float tiempoMuerte = 3f;

    [SerializeField] protected float limiteMapa = 50f;
    [SerializeField] protected float tiempoFueraDelMapa = 3f;

    private Coroutine coroutineDespawn;

    protected virtual void Update()
    {
        if (!spawned)
            return;

        ComprobarLimites();
    }

    public virtual void OnSpawn()
    {
        coroutineDespawn = null;
        spawned = true;
        vida = 1;
    }
    public virtual void OnDespawn()
    {
        coroutineDespawn = null;
        spawned = false;
        StopAllCoroutines();
    }

    public virtual void Morir()
    {
        StopAllCoroutines();
        StartCoroutine(MorirCoroutine());
    }

    public virtual void RecibirDisparo()
    {
        vida--;

        if (vida <= 0)
        {
            Morir();
        }
    }

    protected abstract IEnumerator MorirCoroutine();

    protected void ComprobarLimites()
    {
        if (!spawned)
            return;

        bool fuera =
            Mathf.Abs(transform.position.x) > limiteMapa ||
            Mathf.Abs(transform.position.z) > limiteMapa;

        if (fuera && coroutineDespawn == null)
        {
            coroutineDespawn = StartCoroutine(DespawnFueraDelMapa());
        }
    }

    IEnumerator DespawnFueraDelMapa()
    {
        yield return new WaitForSeconds(tiempoFueraDelMapa);

        if (spawned)
        {
            FactoryObjectPooling.Instance.ReturnObjectToPool(gameObject);
        }
           
    }

}
