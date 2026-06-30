using UnityEngine;

public class ControladorDelJugador : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 10f;
    [SerializeField] private float limiteMapa = 50f;

    Vector3 previousPosition = Vector3.zero;

    private void Awake()
    {

    }
    void Update()
    {
        if (!GameManager.Instance.Jugando)
            return;

        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        bool moviendose =
             Mathf.Abs(movimientoHorizontal) > 0.01f ||
             Mathf.Abs(movimientoVertical) > 0.01f;

        AnimarMovimiento(moviendose);

        Vector3 vectorDePosicion = transform.right * movimientoHorizontal + transform.forward * movimientoVertical;

        vectorDePosicion.Normalize();

        transform.position += vectorDePosicion * velocidadMovimiento * Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > limiteMapa ||
             Mathf.Abs(transform.position.z) > limiteMapa)
        {
            Limites();
        }
    }

    public void AnimarMovimiento(bool corriendo)
    {
        Player.Instance.Animar("Run", corriendo);         
    }

    void Limites()
    {
        Vector3 pos = transform.position;

        transform.position = new Vector3(
            Mathf.Clamp(pos.x, -limiteMapa, limiteMapa),
            pos.y,
            Mathf.Clamp(pos.z, -limiteMapa, limiteMapa)
        );
    }
}
