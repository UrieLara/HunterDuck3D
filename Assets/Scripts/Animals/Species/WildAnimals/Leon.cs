using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leon : AnimalSalvaje
{
    
    Animation leonAnim;

    float velocidadGiro = 5f;

    private readonly string[] atackAnimations =
    {
        "bite",
        "clawsAttackCombo",
        "jumpAttack"
    };

    private void Awake()
    {
        leonAnim = GetComponent<Animation>();
    }

    protected override void Update()
    {
        base.Update();

            switch (estado)
        {
            case EstadoSalvaje.esperando:
                Alejarse();
                break;

            case EstadoSalvaje.corriendo:
                Perseguir();
                break;

        }
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        vida = 2;
        velocidad = 10f;
        distanciaSpawn = 25f;
    }

    void Perseguir()
    {
        leonAnim.Play("run");

        Vector3 objetivo = Player.Instance.transform.position;
        objetivo.y = transform.position.y;

        Quaternion rotacionObjetivo =
        Quaternion.LookRotation(objetivo - transform.position);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            rotacionObjetivo,
            velocidadGiro * Time.deltaTime);

        transform.position +=
            transform.forward *
            velocidad *
            Time.deltaTime;

    }
    protected override IEnumerator RecibirGolpe()
    {
        estado = EstadoSalvaje.golpeado;

        leonAnim.Play("getHit");

        yield return new WaitForSeconds(leonAnim["getHit"].length);

        estado = EstadoSalvaje.corriendo;

    }

    protected override IEnumerator AnimAttack() 
    {
        estado = EstadoSalvaje.atacando;

        velocidad = 0f;

        string animacion = atackAnimations[Random.Range(0, atackAnimations.Length)];

        leonAnim.Play(animacion);

        CameraEffects.Instance.DamageEffect();

        yield return new WaitForSeconds(leonAnim[animacion].length);

        StartCoroutine(EsperarAntesDeAtacar());
    }

    private void Alejarse()
    {
        leonAnim.Play("walk");
        
        transform.position += -transform.forward * velocidad/4 * Time.deltaTime;

    }

    private IEnumerator EsperarAntesDeAtacar()
    {
        estado = EstadoSalvaje.esperando;
        velocidad = 15f;

        yield return new WaitForSeconds(1f);

        estado = EstadoSalvaje.corriendo;
    }

    protected override IEnumerator MorirCoroutine()
    {
        estado = EstadoSalvaje.muriendo;

        leonAnim.Stop();
        leonAnim.Play("death");

        yield return new WaitForSeconds(leonAnim["death"].length + tiempoMuerte);

        FactoryAnimalsPooling.Instance.ReturnObjectToPool(gameObject);
    }
}
