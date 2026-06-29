using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimalObjetivo;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    [SerializeField]
    private List<Objetivo> objetivos;

    private void Awake()
    {
        if (Instance == null)
        Instance = this;
    }

    private void Start()
    {
        InicializarListado();
        HUD.Instance.ActualizarObjetivos(objetivos);  
    }

    private void InicializarListado()
    {
        foreach (Objetivo objetivo in objetivos)
        {
            if (objetivo == null)
            {
                return;
            }

            objetivo.InicializarRandom();
        }
        
    }
    public void ActualizarCaza(TipoAnimal tipo)
    {
        Objetivo objetivo =
            objetivos.Find(objetivo => objetivo.tipoAnimal == tipo);

        if (objetivo == null)
        {
            return;
        }

        objetivo.AumentarCantActual();

        HUD.Instance.ActualizarObjetivos(objetivos);

        ComprobarVictoria();
    }

    void ComprobarVictoria()
    {
        foreach (Objetivo objetivo in objetivos)
        {
            if (objetivo.cantidadActual < objetivo.cantidadNecesaria)
                return;
        }

        GameManager.Instance.CambiarEstado(EstadoJuego.Victoria);
    }

}
