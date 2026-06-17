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

        // 5. RECOMPENSA: Sumamos las monedas directamente al GameManager persistente
        ControladorPico.Instancia.monedasActuales += monedasAlRomper;
        Debug.Log($" ¡Ganaste {monedasAlRomper} monedas! Total actual: {ControladorPico.Instancia.monedasActuales}");

        // Aquí puedes instanciar partículas de destrucción o soltar un ítem antes de destruir el objeto

        Destroy(gameObject);
    }
}