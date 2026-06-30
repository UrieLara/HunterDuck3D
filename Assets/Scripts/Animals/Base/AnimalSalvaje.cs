using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AnimalSalvaje : AnimalBehaviour
{
    protected enum EstadoSalvaje
    {
        esperando,
        corriendo,
        atacando,
        golpeado,
        muriendo
    }

    [Header("Ataque")]
    [SerializeField] protected float distanciaSpawn = 20f;
    [SerializeField] protected float tiempoAdvertencia = 3f;

    protected EstadoSalvaje estado;
    Player jugador => Player.Instance;


    protected override void Update()
    {
        base.Update();

        if (estado == EstadoSalvaje.muriendo)
        {
            return;
        }
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        SpawnCercaDelJugador();
        StartCoroutine(AdvertirAtaque());
    }

    protected void SpawnCercaDelJugador()
    {
        Vector2 dir = Random.insideUnitCircle.normalized;

        Vector3 spawnPos =
            jugador.transform.position +
            new Vector3(dir.x, 0f, dir.y) * distanciaSpawn;

        transform.position = spawnPos;

        Vector3 objetivo = jugador.transform.position;
        objetivo.y = transform.position.y;

        transform.LookAt(objetivo);
    }

    protected IEnumerator AdvertirAtaque()
    {
        estado = EstadoSalvaje.esperando;

        yield return HUD.Instance.MostrarWarning(tiempoAdvertencia);

        estado = EstadoSalvaje.corriendo;
    }

    protected override void ComprobarLimites()
    {
        if (estado == EstadoSalvaje.esperando)
            return;

        base.ComprobarLimites();    
    }

    public override void RecibirDisparo()
    {
        if (estado == EstadoSalvaje.golpeado || estado == EstadoSalvaje.muriendo)
            return;

        vida--;

        if (vida > 0)
        {
            StartCoroutine(RecibirGolpe());
            return;
        }

        estado = EstadoSalvaje.muriendo;
        Morir();
    }

    protected abstract IEnumerator RecibirGolpe();

    protected void OnTriggerEnter(Collider other)
    {
        if (estado == EstadoSalvaje.muriendo)
            return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(AnimAttack());
            Player.Instance.RecibirDaño(1);
        }
    }

    protected abstract IEnumerator AnimAttack();

}