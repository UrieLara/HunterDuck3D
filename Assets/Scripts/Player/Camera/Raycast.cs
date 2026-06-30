using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float rango = 30f;
    private bool habDisparo = true;

    void Update()
    {
        if (!GameManager.Instance.Jugando)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (!habDisparo)
                return;

            Disparar();   
        }
    }

    void Disparar()
    {
        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //(0.5, 0.5) es el centro de la cámara

        Player.Instance.Animar("Disparar");
        if (Physics.Raycast(ray, out RaycastHit objetoGolpeado, rango)) {

            AnimalBehaviour animal = objetoGolpeado.collider.GetComponentInParent<AnimalBehaviour>();
          
            if (animal != null) {
                animal.RecibirDisparo();
                return;
            }
        }

        StartCoroutine(EsperarSiguienteDisparo());
    }

    private IEnumerator EsperarSiguienteDisparo()
    {
        yield return new WaitForSeconds(3f);
        habDisparo = true;

    }
}
