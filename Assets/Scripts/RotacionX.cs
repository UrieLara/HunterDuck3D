using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionX : MonoBehaviour
{
    [SerializeField] float sensibilidad = 100f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Esto es para bloquear el cursor en el centro de la pantalla, lo que hace que no se vea el cursor y que el personaje gire al mover el mouse
    }

    void Update()
    {
        float posX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        transform.Rotate(0, posX, 0); //Esto es para rotar el personaje en el eje Y, lo que hace que gire a la izquierda o derecha
    }
}
