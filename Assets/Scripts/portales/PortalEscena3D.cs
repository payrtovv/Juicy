using UnityEngine;
using UnityEngine.SceneManagement; // Obligatorio para cambiar de escena

public class PortalEscena3D : MonoBehaviour
{
    [Header("Configuración del Viaje")]
    [Tooltip("Escribe el nombre EXACTO de la escena a la que quieres ir")]
    [SerializeField] private string nombreEscenaDestino;

    [SerializeField] private string etiquetaJugador = "Player";

    // Se ejecuta cuando el jugador entra al portal
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si lo que entró al portal es el Player o sus piernas
        bool esElJugador = other.CompareTag(etiquetaJugador) ||
                           (other.transform.parent != null && other.transform.parent.CompareTag(etiquetaJugador));

        if (esElJugador)
        {
            ViajarAEscena();
        }
    }

    private void ViajarAEscena()
    {
        // Validamos que no hayas dejado vacía la casilla en el inspector
        if (!string.IsNullOrEmpty(nombreEscenaDestino))
        {
            Debug.Log($" Teletransportando a: {nombreEscenaDestino}");

            // Esta línea de código borra la escena actual y carga la nueva
            SceneManager.LoadScene(nombreEscenaDestino);
        }
        else
        {
            Debug.LogError(" ¡Error! No has escrito el nombre de la escena destino en el Inspector del Portal.");
        }
    }
}