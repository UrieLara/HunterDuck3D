using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] private Image imgDamage;

    private float duracion = 1f;
    private float intensidad = 0.1f;

    public static CameraEffects Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public IEnumerator Shake()
    {
        Vector3 posicionOriginal = transform.localPosition;

        float tiempo = 0f;

        while (tiempo < duracion)
        {
            transform.localPosition =
                posicionOriginal +
                Random.insideUnitSphere * intensidad;

            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = posicionOriginal;
    }

    IEnumerator FlashRojo()
    {
        Color color = imgDamage.color;

        color.a = 0.5f;
        imgDamage.color = color;

        while (color.a > 0f)
        {
            color.a -= Time.deltaTime * 0.25f;
            imgDamage.color = color;

            yield return null;
        }
    }

    public void DamageEffect()
    {
        StartCoroutine(Shake());
        StartCoroutine(FlashRojo());
    }
}
