using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pajaro : Ave
{
    protected override void Caminar()
    {
        float movimientoY = Mathf.Sin(Time.time) * 0.5f;
        Vector3 direccion = new Vector3(1f, movimientoY, 0).normalized;
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);

        anim.Play("fordwardHop");
    }

}
