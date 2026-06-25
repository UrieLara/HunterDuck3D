using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centers : MonoBehaviour
{
    public static Centers Instance;

    public Transform PatosCentroLago;
    public Transform PatosCentroSuelo;

    private void Awake()
    {
        Instance = this;
    }
}
