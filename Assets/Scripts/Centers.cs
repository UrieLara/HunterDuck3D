using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centers : MonoBehaviour
{
    public static Centers Instance;

    [Header("Ubicación de animales")]
    public Transform PatosCentroSuelo;
    public Transform PatosCentroLago;
    

    private void Awake()
    {
        Instance = this;
    }
}
