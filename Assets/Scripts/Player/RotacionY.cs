using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionY : MonoBehaviour
{
    float rotacionY = 0;
    [SerializeField] float sensibilidad = 100f;
    [SerializeField] float angulo = 45f;

    void Update()
    {
        float posY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;
        posY =- posY;
        rotacionY += posY;

        rotacionY = Mathf.Clamp(rotacionY, -30f, 10f); //Esto es para limitar la rotación en el eje X, lo que hace que no se pueda mirar hacia arriba o hacia abajo más de 50 grados
        transform.localRotation = Quaternion.Euler(rotacionY, 0, 0);
    }
}
