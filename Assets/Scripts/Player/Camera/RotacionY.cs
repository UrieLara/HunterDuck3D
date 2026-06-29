using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionY : MonoBehaviour
{
    float rotacionY = 0;
    [SerializeField] float sensibilidad = 100f;
    [SerializeField] float angulo = 20f;

    [Header("Ángulos")]
    [SerializeField] float anguloMin = -30f;
    [SerializeField] float anguloMax = 20f;

    void Update()
    {
        if (!GameManager.Instance.Jugando)
            return;

        float posY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;
        posY =- posY;
        rotacionY += posY;

        rotacionY = Mathf.Clamp(rotacionY, anguloMin, anguloMax); //Esto es para limitar la rotación en el eje X, lo que hace que no se pueda mirar hacia arriba o hacia abajo más de 50 grados
        transform.localRotation = Quaternion.Euler(rotacionY, 0, 0);
    }
}
