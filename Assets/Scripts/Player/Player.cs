using KevinIglesias;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

   // public Animator archerAnimator;

    private int vida = 30;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

      //  archerAnimator = GetComponentInChildren<Animator>();

    }

    public void Animar(string animacion)
    {
      //  archerAnimator.SetTrigger(animacion);
    }

    public void RecibirDaño(int daño)
    {
        vida -= daño;

        HUD.Instance.ActualizarVidas(vida);

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        GameManager.Instance.CambiarEstado(EstadoJuego.Derrota);
    }

    public int GetVidas()
    {
        return vida;
    }
}
