using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimalObjetivo;

[System.Serializable]
public class Objetivo 
{
    public TipoAnimal tipoAnimal;

    public int cantMin = 2;
    public int cantMax = 10;

    [HideInInspector] public int cantidadNecesaria;
    [HideInInspector] public int cantidadActual;

    public void InicializarRandom()
    {
        cantidadNecesaria = Random.Range(cantMin, cantMax + 1);
    }

    public void AumentarCantActual()
    {
        cantidadActual++;
    }

    public string Progreso => $"{cantidadActual}/{cantidadNecesaria}";
}
