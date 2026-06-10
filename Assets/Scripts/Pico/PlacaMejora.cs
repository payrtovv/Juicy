using UnityEngine;

public class PlacaMejora : MonoBehaviour
{
    [Header("Configuración de Detección")]
    [SerializeField] private string etiquetaJugador = "Player";

    private bool jugadorEncima = false;

    // METODO 3D: Se ejecuta cuando algo entra en el Trigger 3D
    private void OnTriggerEnter(Collider other)
    {
        // DEBUG: Esto DEBE salir en consola ahora que es 3D

        // Verificamos si el objeto que chocó O su objeto padre tienen el Tag "Player"
        bool esElJugador = other.CompareTag(etiquetaJugador) ||
                           (other.transform.parent != null && other.transform.parent.CompareTag(etiquetaJugador));

        if (esElJugador && !jugadorEncima)
        {
            jugadorEncima = true;
            IntentarEvolucionarPico();
        }
    }

    // METODO 3D: Se ejecuta cuando el jugador sale del Trigger 3D
    private void OnTriggerExit(Collider other)
    {
        bool esElJugador = other.CompareTag(etiquetaJugador) ||
                           (other.transform.parent != null && other.transform.parent.CompareTag(etiquetaJugador));

        if (esElJugador)
        {
            jugadorEncima = false;
            Debug.Log(" El jugador bajó de la placa.");
        }
    }

    private void IntentarEvolucionarPico()
    {
        ControladorPico pico = ControladorPico.Instancia;

        if (pico == null)
        {
            Debug.LogError(" No se encontró el ControladorPico (GameManager) en la escena.");
            return;
        }

        int costoNecesario = pico.CostoSiguienteMejora;

        if (pico.IntentarMejorarPico())
        {
            Debug.Log($" ¡Mejora Exitosa! Gastaste {costoNecesario} monedas. Nivel de Pico actual: {pico.nivelActual}");
        }
        else
        {
            Debug.LogWarning($" Monedas insuficientes. Necesitas {costoNecesario} y tienes {pico.monedasActuales}.");
        }
    }
}