using UnityEngine;

public class InteractableBlock : MonoBehaviour
{
    [Header("Configuración del Bloque")]
    [Tooltip("Nombre del bloque para identificarlo.")]
    [SerializeField] private string blockName = "Bloque Común";

    [Tooltip("Nivel o rareza del bloque. Puede servir para futuras mecánicas (ej. requerir herramientas de cierto nivel).")]
    [SerializeField] private int blockLevel = 1;

    [Tooltip("Cantidad de clics necesarios para romper este bloque.")]
    [SerializeField] private int maxClicksRequired = 3;

    // Contador interno de los impactos recibidos
    private int currentClicksReceived = 0;

    /// <summary>
    /// Método que llamará el jugador cuando golpee este objeto.
    /// </summary>
    public void ReceiveHit()
    {
        currentClicksReceived++;

        Debug.Log($"¡Golpe a {blockName}! Resistencia: {currentClicksReceived}/{maxClicksRequired} (Nivel {blockLevel})");

        // Comprobar si ya se alcanzaron los clics necesarios
        if (currentClicksReceived >= maxClicksRequired)
        {
            BreakBlock();
        }
    }

    private void BreakBlock()
    {
        Debug.Log($"El bloque '{blockName}' ha sido destruido.");

        // Aquí puedes instanciar partículas de destrucción o soltar un ítem antes de destruir el objeto

        Destroy(gameObject);
    }
}