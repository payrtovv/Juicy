using TMPro;
using UnityEngine;

public class PlacaMejora : MonoBehaviour
{
    [Header("Configuración de Detección")]
    [SerializeField] private string etiquetaJugador = "Player";

    [Header("Configuración de Audio")]
    public AudioSource audioSource;       // El componente que reproducirá los sonidos
    public AudioClip sonidoMejoraExitosa; // Arrastra aquí el sonido de éxito (ej. campanas, monedas)
    public AudioClip sonidoMejoraFallida;

    [Header("Configuración de UI")]
    public TextMeshProUGUI textoCosto;

    private bool jugadorEncima = false;

    



    private void OnTriggerEnter(Collider other)
    {

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
            ReproducirSonido(sonidoMejoraExitosa);
            ActualizarTextoCosto(pico.CostoSiguienteMejora);

        }
        else
        {
            Debug.LogWarning($" Monedas insuficientes. Necesitas {costoNecesario} y tienes {pico.monedasActuales}.");
            ReproducirSonido(sonidoMejoraFallida);
        }
    }
    private void ReproducirSonido(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void ActualizarTextoCosto(int nuevoCosto)
    {
        if (textoCosto != null)
        {
            // Cambia el texto. Puedes usar un formato como "150" o "Costo: 150"
            textoCosto.text = $"{nuevoCosto} monedas";
        }
    }
}