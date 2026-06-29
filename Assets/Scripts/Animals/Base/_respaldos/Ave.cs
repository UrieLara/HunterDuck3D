using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ave : MonoBehaviour
{
    protected float tamaño;
    [SerializeField] protected float velocidad;

    protected Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
        //tamaño = Random.Range(1f, 6f);
        //transform.localScale = Vector3.one * tamaño;
    }

    //virtual: Permite que las clases hijas puedan modificar el método, pero no es obligatorio
    //abstract: Sirve para obligar a las clases hijas a implementar el método, no se puede usar en la clase padre
    protected virtual void Caminar()
    {
       transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
       anim.Play("walk");
    }

    protected virtual void Volar()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        anim.Play("fly");
    }
}
