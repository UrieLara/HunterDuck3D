using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
    using UnityEngine.SceneManagement;
using UnityEngine;


public enum EstadoJuego {
    MenuPrincipal,
    Jugando,
    Victoria,
    Derrota

    }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EstadoJuego Estado { get; private set; }

    public bool Jugando => Estado == EstadoJuego.Jugando;

    private float tiempoJuego = 0f;
    public float TiempoJuego => tiempoJuego;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }   
        else
        {
            Destroy(gameObject);
        }
            
    }

    private void Start()
    {
        CambiarEstado(EstadoJuego.MenuPrincipal);
    }

    private void Update()
    {
        ActivarCronometro();
    }

    public void CambiarEstado(EstadoJuego nuevoEstado)
    {
        Estado = nuevoEstado;

        ConfigurarCursor();

        switch (Estado)
        {
            case EstadoJuego.MenuPrincipal:
                EntrarMenu();
                break;

            case EstadoJuego.Jugando:
                EntrarEnJuego();
                break;

            case EstadoJuego.Victoria:
                Ganar();
                break;

            case EstadoJuego.Derrota:
                Perder();
                break;
        }
    }

    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    void EntrarMenu()
    {
        CargarEscena("MenuPrincipal");
    }

    public void EntrarEnJuego()
    {
        Time.timeScale = 1f;
        CargarEscena("GameScene");
        tiempoJuego = 0f; 
    }

    void Ganar()
    {
        HUD.Instance.EscribirFinPartida("Ganaste!");
        Time.timeScale = 0f;
    }

    void Perder()
    {
        HUD.Instance.EscribirFinPartida("Perdiste");
        Time.timeScale = 0f;
    }
    private void ConfigurarCursor()
    {
        bool bloquear = Estado == EstadoJuego.Jugando;

        Cursor.lockState = bloquear
            ? CursorLockMode.Locked
            : CursorLockMode.None;

        Cursor.visible = !bloquear;
    }

    private void ActivarCronometro()
    {
        if (Estado != EstadoJuego.Jugando)
            return;

        tiempoJuego += Time.deltaTime;
    }
}
