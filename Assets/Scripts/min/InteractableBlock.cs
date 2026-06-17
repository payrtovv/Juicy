using UnityEngine;

public class InteractableBlock : MonoBehaviour
{
    [Header("Configuración del Bloque")]
    [Tooltip("Nombre del bloque para identificarlo.")]
    [SerializeField] private string blockName = "Bloque Común";

    [Tooltip("Nivel o rareza del bloque. Puede servir para futuras mecánicas.")]
    [SerializeField] private int blockLevel = 1;

    [Tooltip("Resistencia o vida base del bloque.")]
    [SerializeField] private int maxClicksRequired = 3;

    [Tooltip("Cantidad de monedas que otorga este bloque al destruirse.")]
    [SerializeField] private int monedasAlRomper = 10;

    [Header("Efectos de Sonido")]
    [Tooltip("Arrastra aquí el archivo de audio (.mp3 o .wav) del sonido de la moneda.")]
    [SerializeField] private AudioClip sonidoMoneda;

    // Contador interno para saber cuánta vida le queda al bloque
    private int currentHealth;

    private void Start()
    {
        // Al iniciar, la vida actual del bloque es su resistencia base
        currentHealth = maxClicksRequired;
    }

    /// <summary>
    /// Método que llamará el jugador cuando golpee este objeto.
    /// </summary>
    public void ReceiveHit()
    {
        // 1. Validamos que el GameManager exista en la escena
        if (ControladorPico.Instancia == null)
        {
            Debug.LogError("No se encontró el ControladorPico (GameManager) en la escena.");
            return;
        }

        // 2. ABSORBEMOS LA FUERZA: Tomamos la fuerza de minado actual del GameManager
        int fuerzaDelPico = ControladorPico.Instancia.fuerzaMinado;

        // 3. Restamos la fuerza a la vida del bloque
        currentHealth -= fuerzaDelPico;

        Debug.Log($"¡Golpe a {blockName}! Fuerza aplicada: {fuerzaDelPico} | Vida restante del bloque: {Mathf.Max(0, currentHealth)}/{maxClicksRequired}");

        // 4. Comprobar si el bloque se quedó sin vida
        if (currentHealth <= 0)
        {
            BreakBlock();
        }
    }

    private void BreakBlock()
    {
        Debug.Log($"El bloque '{blockName}' ha sido destruido.");

        // 1. REPRODUCIR SONIDO: Si asignaste un audio, lo reproduce en la posición del cubo
        if (sonidoMoneda != null)
        {
            // Crea un emisor invisible que no se corta cuando el bloque se destruye
            AudioSource.PlayClipAtPoint(sonidoMoneda, transform.position);
        }
        else
        {
            Debug.LogWarning($"No has asignado el sonido de moneda en el bloque: {blockName}");
        }

        // 2. RECOMPENSA: Sumamos las monedas directamente al GameManager persistente
        ControladorPico.Instancia.monedasActuales += monedasAlRomper;
        Debug.Log($" ¡Ganaste {monedasAlRomper} monedas! Total actual: {ControladorPico.Instancia.monedasActuales}");

        // 3. TEXTO FLOTANTE: Enviamos la información al Canvas
        if (CoinCanvasManager.Instancia != null)
        {
            CoinCanvasManager.Instancia.SpawnCoinText(monedasAlRomper);
        }
        else
        {
            Debug.LogWarning("No se encontró CoinCanvasManager en la escena para mostrar el texto.");
        }

        // 4. DESTRUCCIÓN: Desaparece el bloque del mapa
        Destroy(gameObject);
    }
}