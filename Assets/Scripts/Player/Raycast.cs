using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float rango = 1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //(0.5, 0.5) es el centro de la cámara
        
        if (Physics.Raycast(ray, out RaycastHit objetoGolpeado, rango)) {

            AnimalBehaviour animal = objetoGolpeado.collider.GetComponentInParent<AnimalBehaviour>();
          
            if (animal != null) {
                animal.RecibirDisparo();
                return;
            }
        }

    }
}
