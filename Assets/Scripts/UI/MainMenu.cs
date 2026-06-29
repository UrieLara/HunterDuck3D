using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputNombre;
    [SerializeField] private Button botonJugar;

    private void Start()
    {
        botonJugar.interactable = false;

        inputNombre.onValueChanged.AddListener(ComprobarNombre);
    }

    void ComprobarNombre(string nombre)
    {
        botonJugar.interactable = !string.IsNullOrWhiteSpace(nombre);
    }

    public void OnClickJugar()
    {
        PlayerData.Nombre = inputNombre.text.Trim();

        GameManager.Instance.CambiarEstado(EstadoJuego.Jugando);
    }
}
