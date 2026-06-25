using UnityEngine;

public class ControladorDelJugador : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float tamSuelo = 50f;

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 vectorDePosicion = transform.right * movimientoHorizontal + transform.forward * movimientoVertical;

        transform.position += vectorDePosicion * velocidadMovimiento * Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > tamSuelo ||
             Mathf.Abs(transform.position.z) > tamSuelo)
        {
            Limites();
        }
    }

    void Limites()
    {
        Vector3 pos = transform.position;

        transform.position = new Vector3(
            Mathf.Clamp(pos.x, -tamSuelo, tamSuelo),
            pos.y,
            Mathf.Clamp(pos.z, -tamSuelo, tamSuelo)
        );
    }
}
