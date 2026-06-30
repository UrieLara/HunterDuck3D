
using UnityEngine.SceneManagement;
using UnityEngine;


public enum EstadoJuego {
    MenuPrincipal,
    Jugando,
    Pausa,
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
        if (Estado != EstadoJuego.Jugando)
            return;

        ActivarCronometro();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Estado == EstadoJuego.Jugando)
                CambiarEstado(EstadoJuego.Pausa);

            else if (Estado == EstadoJuego.Pausa)
                CambiarEstado(EstadoJuego.Jugando);
        }
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

            case EstadoJuego.Pausa:
                EntrarPausa();
                break;
        }

        
    }

    public void NuevaPartida()
    {
        tiempoJuego = 0f;
        Time.timeScale = 1f;

        CargarEscena("GameScene");

        CambiarEstado(EstadoJuego.Jugando);
    }

    void EntrarEnJuego()
    {
        Time.timeScale = 1f;
    }

    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    void EntrarMenu()
    {
        CargarEscena("MenuPrincipal");
    }

    void EntrarPausa()
    {
        HUD.Instance.MostrarPausa();
        Time.timeScale = 0f;
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
