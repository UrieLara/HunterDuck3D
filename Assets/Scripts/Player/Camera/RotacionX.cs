using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionX : MonoBehaviour
{
    [SerializeField] float sensibilidad = 100f;

    void Start()
    {
        //GameManager.Instance.BloquearCursor(true);
    }

    void Update()
    {
        if (!GameManager.Instance.Jugando)
            return;

        float posX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        transform.Rotate(0, posX, 0); //Esto es para rotar el personaje en el eje Y, lo que hace que gire a la izquierda o derecha
    }
}
