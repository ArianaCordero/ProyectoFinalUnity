using UnityEngine;

public class PuertaInteractiva : MonoBehaviour
{
    public float anguloApertura = 90f;
    public float velocidad = 2f;
    public KeyCode teclaInteraccion = KeyCode.E;
    public float distanciaInteraccion = 3f;
    public Transform jugador;

    private bool estaAbierta = false;
    private bool jugadorCerca = false;
    private Quaternion rotacionCerrada;
    private Quaternion rotacionAbierta;
    private Quaternion rotacionObjetivo;

    void Start()
    {
        rotacionCerrada = transform.rotation;
        rotacionAbierta = rotacionCerrada * Quaternion.Euler(0, anguloApertura, 0);
        rotacionObjetivo = rotacionCerrada;
    }

    void Update()
    {
        if (jugador != null)
        {
            float distancia = Vector3.Distance(transform.position, jugador.position);
            jugadorCerca = distancia <= distanciaInteraccion;
        }

        if (jugadorCerca && Input.GetKeyDown(teclaInteraccion))
            AlternarPuerta();

        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidad);
    }

    void AlternarPuerta()
    {
        estaAbierta = !estaAbierta;
        rotacionObjetivo = estaAbierta ? rotacionAbierta : rotacionCerrada;
    }

    void OnMouseDown()
    {
        if (jugadorCerca)
            AlternarPuerta();
    }
}