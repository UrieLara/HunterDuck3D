using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static MissionManager;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI textoNombre;
    [SerializeField] private TextMeshProUGUI vidaJugador;
    [SerializeField] private TextMeshProUGUI TextObjetivos;
    [SerializeField] private TextMeshProUGUI cronometro;
    [SerializeField] private GameObject warning;
    

    [Header("GameOver")]
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI TextGameOver;

    private int ultimoSegundo = -1;

    private void Awake()
    {
        if (Instance == null)
        Instance = this;
    }

    private void Start()
    {
        textoNombre.text = $"Lista de Caza de {PlayerData.Nombre}";
        vidaJugador.text = $"Vidas: {Player.Instance.GetVidas()}";
        warning.SetActive(false);
        panelGameOver.SetActive(false);
    }

    private void Update()
    {
        ActualizarCronometro();
    }

    public void ActualizarVidas(int vidas)
    {
        vidaJugador.text = $"Vidas: {vidas}";

        if (vidas < 2) {
            vidaJugador.color = Color.red;
            AnimarUI(vidaJugador.transform, 5f);
        }
        else
        {
            vidaJugador.color = Color.white;
        }
    }

    public IEnumerator MostrarWarning(float tiempo)
    {
        warning.SetActive(true);
        AnimarUI(warning.transform, tiempo);

        yield return new WaitForSeconds(tiempo);

        warning.SetActive(false);
    }

    public void ActualizarObjetivos(List<Objetivo> objetivos)
    {
        string temp = "";

        foreach (Objetivo objetivo in objetivos)
        {
            temp += $"{objetivo.tipoAnimal}: {objetivo.Progreso}\n";
        }
        TextObjetivos.text = temp;
    }

    public void AnimarUI(Transform objeto, float duracion)
    {
        StartCoroutine(AnimacionEscala(objeto, duracion));
    }

    IEnumerator AnimacionEscala(Transform elemento, float duracion)
    {
        Vector3 escalaOriginal = elemento.localScale;
        Vector3 escalaMaxima = escalaOriginal * 1.2f;

        float frecuencia = 4f; // 4 pulsos por segundo

        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float t = Mathf.PingPong(tiempo * frecuencia * 2f, 1f);

            elemento.localScale = Vector3.Lerp(
                escalaOriginal,
                escalaMaxima,
                t);

            tiempo += Time.deltaTime;
            yield return null;
        }

        elemento.localScale = escalaOriginal;
    }

    public void EscribirFinPartida(string text)
    {
        panelGameOver.SetActive(true);
        TextGameOver.text = text;
    }

    public void OnClickReiniciar()
    {
        GameManager.Instance.CambiarEstado(EstadoJuego.Jugando);
    }

    private void ActualizarCronometro()
    {
        int segundoActual = Mathf.FloorToInt(GameManager.Instance.TiempoJuego);

        if (segundoActual == ultimoSegundo)
            return;

        ultimoSegundo = segundoActual;
        cronometro.text = FormatearTiempo(GameManager.Instance.TiempoJuego);
    }

    string FormatearTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);

        return $"{minutos:00}:{segundos:00}";
    }
}
