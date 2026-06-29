using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalObjetivo : AnimalBehaviour
{
    public enum TipoAnimal
    {
        Cuervo,
        Pato,
        Zebra 
    }

    [SerializeField] protected TipoAnimal tipoAnimal;

    protected void RegistrarMuerte()
    {
        MissionManager.Instance.ActualizarCaza(tipoAnimal);
    }
}
