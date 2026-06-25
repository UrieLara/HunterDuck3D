using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuervo : Ave
{

    protected Animator animatorCuervo;

    private void Start()
    {
        animatorCuervo = GetComponent<Animator>();
    }
    void Update()
    {
       Volar();
    }

    protected override void Volar()
    {
        float movimientoY = Mathf.Sin(Time.time) * 0.8f;
        Vector3 direccion = new Vector3(0, movimientoY, velocidad).normalized;
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);

        animatorCuervo.SetBool("flying", true);
    }
}
